using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Underwater : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public Material material;
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingSkybox;
        public Color color;
        public float distance = 10;
        [Range(0, 1)]
        public float alpha;
        public float refraction = 0.1f;
        public Texture normalmap;
        public Vector4 UV = new Vector4(1, 1, 0.2f, 0.1f);
    }

    public Settings settings = new Settings();

    class Pass : ScriptableRenderPass
    {
        public Settings settings;
        private RenderTargetIdentifier source;
        public RTHandle tempTexture;
        private string profilerTag;

        public void Setup(RenderTargetIdentifier source)
        {
            this.source = source;
        }

        public Pass(string profilerTag)
        {
            this.profilerTag = profilerTag;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            if (tempTexture == null)
            {
                tempTexture = RTHandles.Alloc(
                    cameraTextureDescriptor.width,
                    cameraTextureDescriptor.height,
                    colorFormat: cameraTextureDescriptor.graphicsFormat,
                    useDynamicScale: false,
                    name: "UnderwaterTempTexture"
                );
            }
            ConfigureTarget(tempTexture);
            ConfigureClear(ClearFlag.All, Color.black);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);
            cmd.Clear();

            try
            {
                // Set material properties
                settings.material.SetFloat("_dis", settings.distance);
                settings.material.SetFloat("_alpha", settings.alpha);
                settings.material.SetColor("_color", settings.color);
                settings.material.SetTexture("_NormalMap", settings.normalmap);
                settings.material.SetFloat("_refraction", settings.refraction);
                settings.material.SetVector("_normalUV", settings.UV);

                // Blit operations
                Blit(cmd, source, tempTexture);
                Blit(cmd, tempTexture, source, settings.material, 0);

                context.ExecuteCommandBuffer(cmd);
            }
            catch (Exception e)
            {
                Debug.LogError($"Underwater effect error: {e.Message}");
            }

            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            if (tempTexture != null)
            {
                RTHandles.Release(tempTexture);
                tempTexture = null;
            }
        }
    }

    Pass pass;

    public override void Create()
    {
        pass = new Pass("Underwater Effects");
        name = "Underwater Effects";
        pass.settings = settings;
        pass.renderPassEvent = settings.renderPassEvent;
    }

    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
        var cameraColorTargetIdent = renderer.cameraColorTarget;
        pass.Setup(cameraColorTargetIdent);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(pass);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (pass != null && pass.tempTexture != null)
        {
            RTHandles.Release(pass.tempTexture);
        }
    }
}

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Water_Volume : ScriptableRendererFeature
{
    class CustomRenderPass : ScriptableRenderPass
    {
        public RTHandle source;

        private Material _material;
        private RTHandle tempRenderTarget;

        public CustomRenderPass(Material mat)
        {
            _material = mat;
            tempRenderTarget = RTHandles.Alloc("_TemporaryColourTexture", name: "_TemporaryColourTexture");
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(
                cameraTextureDescriptor.colorFormat,
                RenderTextureReadWrite.Default
            );

            tempRenderTarget = RTHandles.Alloc(
                cameraTextureDescriptor.width,
                cameraTextureDescriptor.height,
                depthBufferBits: 0,
                colorFormat: graphicsFormat,
                dimension: TextureDimension.Tex2D,
                useDynamicScale: true,
                name: "_TemporaryColourTexture"
            );
        }


        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (renderingData.cameraData.cameraType != CameraType.Reflection)
            {
                CommandBuffer cmd = CommandBufferPool.Get("Water Volume Effect");

                // Render the effect.
                Blit(cmd, source, tempRenderTarget, _material);
                Blit(cmd, tempRenderTarget, source);

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            // Release the temporary render target.
            if (tempRenderTarget != null)
            {
                RTHandles.Release(tempRenderTarget);
                tempRenderTarget = null;
            }
        }
    }

    [System.Serializable]
    public class _Settings
    {
        public Material material = null;
        public RenderPassEvent renderPass = RenderPassEvent.AfterRenderingSkybox;
    }

    public _Settings settings = new _Settings();
    CustomRenderPass m_ScriptablePass;

    public override void Create()
    {
        if (settings.material == null)
        {
            settings.material = (Material)Resources.Load("Water_Volume");
        }

        m_ScriptablePass = new CustomRenderPass(settings.material);
        m_ScriptablePass.renderPassEvent = settings.renderPass;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        m_ScriptablePass.source = renderer.cameraColorTargetHandle;
        renderer.EnqueuePass(m_ScriptablePass);
    }
}

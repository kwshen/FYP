using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public ParticleSystem waterSplash;  // Assign the Particle System in the Inspector
    public Transform leftPaddleTip;         // Assign the paddle tip Transform
    public Transform rightPaddleTip;         // Assign the paddle tip Transform

    public Paddling paddlingScript;
    private bool playedSplashEffect = false;

    void Update()
    {
        // if is paddling do water splash
        if (paddlingScript.getIsPaddling() == true && playedSplashEffect == false)
        {
            playedSplashEffect=true;
            //check which paddletip under water
            if (paddlingScript.getLeftPaddleUnderWater() == true)
                PlaySplashEffect(leftPaddleTip.position);  // Play the splash effect at the hit point
            else if(paddlingScript.getRightPaddleUnderWater() == true)
                PlaySplashEffect(rightPaddleTip.position);
        }
        else
        {
            playedSplashEffect = false;
        }
        
    }

    void PlaySplashEffect(Vector3 position)
    {
        // Move the Particle System to the interaction point
        waterSplash.transform.position = position;

        // Play the Particle System
        if (!waterSplash.isPlaying)
        {
            waterSplash.Play();
        }
    }
}

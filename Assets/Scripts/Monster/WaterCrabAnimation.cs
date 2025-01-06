public class WaterCrabAnimation : CrabAnimation
{
    WaterCrabAudio waterCrabAudio;

    // Start is called before the first frame update
    void Start()
    {
        waterCrabAudio = GetComponent<WaterCrabAudio>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void playSpecialMoveSound()
    {
        waterCrabAudio.playJumpSound();
    }
}

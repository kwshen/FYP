public class WaterCrabAnimation : CrabAnimation
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void playSpecialMoveSound()
    {
        AudioManager.Instance.PlaySFX("CrabComeOut");
    }
}

public class NormalCrabAnimation : CrabAnimation
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        crabAnimator.SetInteger("state", 1);
    }

    protected override void playSpecialMoveSound()
    {

    }
}

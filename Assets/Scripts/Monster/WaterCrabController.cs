using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public  class WaterCrabController : CrabController
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

    protected override void specialMove()
    {
        heightNeedToJump = river.transform.position.y - gameObject.transform.position.y;

        rb.AddForce(new Vector3(0, heightNeedToJump + 1, 0), ForceMode.Impulse);

    }

    protected override void ResetStatus()
    {
        if (isOnWater())
        {
            appearCollision.setOnWater(true);
            appearCollision.setAppear(false);
            enableAgent();
            isSpecialMoving = false;
            onWater = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPump : NewtonVR.NVRSlider
{
    bool InPump;
    bool OutPump;
    public bool pumped = false;
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!pumped)
        {
            if (OutPump && InPump){ pumped = true; return;  }

            if (InPump && CurrentValue < 0.1f) { OutPump = true; return; }
            if (!InPump && CurrentValue > 0.9f){ InPump = true; return; }
        }
    }

}

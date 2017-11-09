using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorState : MonoBehaviour {

    public void LockFloor( ) {
        gameObject.layer = 0;
    }

    public void UnlockFloor( ) {
        gameObject.layer = 15;

    }


}

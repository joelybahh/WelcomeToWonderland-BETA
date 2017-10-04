using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_UnlockSecondStage : MonoBehaviour {
	public void UnlockRoom() {
        gameObject.tag = "Teleportable";
    }
}

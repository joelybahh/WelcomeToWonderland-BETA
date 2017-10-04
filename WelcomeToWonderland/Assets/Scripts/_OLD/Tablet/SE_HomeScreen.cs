using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_HomeScreen : MonoBehaviour {
    public void UpdateHomeScreen(Collider a_other, string a_notesTag, string a_cameraTag) {
        if (a_other.tag == a_notesTag) {
            SE_TabletScreenState.SwapToState(SE_TabletScreenState.eScreenState.NOTES_APP);
        } else if(a_other.tag == a_cameraTag) {
            SE_TabletScreenState.SwapToState(SE_TabletScreenState.eScreenState.CAMERA_APP);
        }
    }
}

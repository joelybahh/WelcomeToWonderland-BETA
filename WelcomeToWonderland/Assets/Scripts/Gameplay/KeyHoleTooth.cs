using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHoleTooth : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if ( other.gameObject.tag == "LevelKey"  ) {
            GetComponent<KeyHole>().turned = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if ( other.gameObject.tag == "LevelKey" ) {
            GetComponent<KeyHole>().turned = false;
        }
    }
}

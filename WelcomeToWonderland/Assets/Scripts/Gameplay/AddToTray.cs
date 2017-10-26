using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToTray : MonoBehaviour {

    void OnTriggerEnter ( Collider other ) {
        if (other.gameObject.layer == 25) {
            other.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
        }
    }
    void OnTriggerExit ( Collider other ) {
        if (other.gameObject.layer == 25) {

            other.gameObject.transform.parent = null;
            other.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
        }
    }
}

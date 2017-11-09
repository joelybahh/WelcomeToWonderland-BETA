using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumption : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if ( other.tag == "Consumable" ) {
            Destroy(other.gameObject);
        }
    }
}

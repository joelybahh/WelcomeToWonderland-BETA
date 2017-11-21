using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHole : MonoBehaviour {
    public string indexName;
    public bool turned = true;
	// Use this for initialization
	void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag == "LevelKey" && other.gameObject.GetComponent<LevelKey>().Name == indexName ) {
            var c = other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition  | 
                                                                     RigidbodyConstraints.FreezeRotationY | 
                                                                     RigidbodyConstraints.FreezeRotationZ;

            if( (c & RigidbodyConstraints.FreezePosition) == RigidbodyConstraints.FreezePosition ) {

            }

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMirror : MonoBehaviour {
    public Transform m_heightRef;
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, m_heightRef.position.y, transform.position.z);
	}

    void FixedUpdate() {
        
    }
}

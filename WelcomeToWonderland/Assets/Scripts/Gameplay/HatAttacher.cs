using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatAttacher : MonoBehaviour {

    [SerializeField] private Rigidbody m_rb;

	void OnTriggerEnter(Collider col) {
        if(col.tag == "HatAttach") {
            transform.parent = col.transform;
            transform.position = col.transform.position;
            transform.rotation = Quaternion.identity;
            m_rb.isKinematic = true;
        }
    }

    void OnTriggerExit ( Collider col ) {
        if (col.tag == "HatAttach") {
            transform.parent = null;
            m_rb.isKinematic = false;
        }
    }
}

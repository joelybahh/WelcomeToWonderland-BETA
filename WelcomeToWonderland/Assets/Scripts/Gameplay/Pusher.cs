using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour {

    public float m_pushDownForce = 60.0f;
    public Vector3 m_dir;

	void OnTriggerStay(Collider col) {
        Rigidbody rbRef = col.GetComponent<Rigidbody>();

        if(rbRef != null) {
            rbRef.AddForce(m_dir * m_pushDownForce * Time.fixedDeltaTime);
        }
    }
}

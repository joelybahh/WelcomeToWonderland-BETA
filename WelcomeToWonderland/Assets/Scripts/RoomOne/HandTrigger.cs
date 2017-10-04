using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandTrigger : MonoBehaviour {

    public bool m_started = false;
    public List<Rigidbody> m_robotPieces;
    public UnityEvent OnEnter;
	void OnTriggerEnter(Collider col) {
        if(col.tag == "PowerGlove") {
            OnEnter.Invoke();
            
        }
    }

    void DestroyRobot() {
        foreach(Rigidbody r in m_robotPieces) {
           // r.useGravity = true;
            //r.isKinematic = false;
        }
    }
}

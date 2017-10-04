using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostAndFoundCatcher : MonoBehaviour {
    public Transform m_spawnPos;

    void OnTriggerEnter(Collider col)
    {
        Rigidbody rbRef = col.GetComponent<Rigidbody>();
        if(rbRef != null) {
            col.transform.position = m_spawnPos.position;
        }
    }	
}
// !interacting or teleporting = IDLE
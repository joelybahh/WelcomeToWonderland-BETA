using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetDirectionHandler : MonoBehaviour {

    [SerializeField] private GameObject m_IdleRef;
    [SerializeField] private GameObject m_NorthRef;
    [SerializeField] private GameObject m_SouthRef;
	
	void Update () {
		if(Input.GetKey(KeyCode.A)) {
            SetDirection (eDirection.SOUTH);
        } else if(Input.GetKey(KeyCode.D)) {
            SetDirection (eDirection.NORTH);
        } else {
            SetDirection (eDirection.IDLE);
        }
	}

    private void SetDirection(eDirection a_direction) {
        switch(a_direction) {
            case eDirection.NORTH:
                m_NorthRef.SetActive (true);
                m_SouthRef.SetActive (false);
                m_IdleRef.SetActive  (false);
                break;
            case eDirection.SOUTH:
                m_NorthRef.SetActive (false);
                m_SouthRef.SetActive (true);
                m_IdleRef.SetActive (false);
                break;
            case eDirection.IDLE:
                m_NorthRef.SetActive (false);
                m_SouthRef.SetActive (false);
                m_IdleRef.SetActive (true);
                break;
        }
    }
}

public enum eDirection {
    NORTH,
    SOUTH,
    IDLE
}

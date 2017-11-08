using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class TrailerIntro : MonoBehaviour {

    [SerializeField] private Rigidbody m_rbOne;
    [SerializeField] private Rigidbody m_rbTwo;

    void Update () {
        if (Input.GetKeyDown (KeyCode.F5)) {
            m_rbOne.gameObject.SetActive (true);
            m_rbOne.isKinematic = false;
        }
        if (Input.GetKeyDown (KeyCode.F6)) {
            m_rbTwo.gameObject.SetActive (true);
            m_rbTwo.isKinematic = false;
        }
    }
}

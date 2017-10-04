using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Physics {
    /// <summary>
    /// Desc: Pushes any gumballs in the trigger zone, downwards, funelling them out the dispenser.
    /// Author: Joel Gabriel
    /// </summary>
    public class GumballPusher : MonoBehaviour {

        [SerializeField] private GumballDispenser m_dispenser;
        [SerializeField] private float m_pushDownForce = 5.0f;

        private void OnTriggerStay(Collider col) {
            if(col.tag == "Gumball") {
                if (m_dispenser.m_twistState == eTwistState.OPENED) {
                    col.GetComponent<Rigidbody>().AddForce(Vector3.down * m_pushDownForce);
                }
            }
        }
    }
}
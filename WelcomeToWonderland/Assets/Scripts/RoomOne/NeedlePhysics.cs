using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Physics {
    public class NeedlePhysics : MonoBehaviour {

        private HingeJoint m_hinge;

        [SerializeField]
        private float MaxImpactVelocity;

        [SerializeField]
        private HingeJoint m_spinnerHinge;

        void Start() {
            m_hinge = GetComponent<HingeJoint>();
        }

        void OnCollisionEnter( Collision a_other ) {
            if ( a_other.collider.tag == "Stopper" ) {
                //m_hinge.useSpring = false;
                //m_spinnerHinge.useMotor = true;
            }
        }

        void OnCollisionExit( Collision a_other ) {
            if ( a_other.collider.tag == "Stopper" ) {
                //m_hinge.useSpring = true;
                //m_spinnerHinge.useMotor = false;
            }
        }
    }
}
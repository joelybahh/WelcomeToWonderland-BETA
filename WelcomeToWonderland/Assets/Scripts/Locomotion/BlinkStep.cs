using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Loco.VR.Input;

namespace Loco.VR.Movement {
    public class BlinkStep : MonoBehaviour {

        [Header("VR Settings")]
        [SerializeField]
        private Transform m_vrRigRef;
        [SerializeField]
        private VRBasicController m_leftContRef;
        [SerializeField]
        private VRBasicController m_rightContRef;

        [SerializeField]
        private LayerMask m_mask;

        [Header("Blink Direction Settings")]
        [SerializeField]
        private Transform m_directionRef;

        [Header("Distance Settings")]
        [SerializeField]
        private float m_blinkDistance;

        [Header("Blink Events")]
        [SerializeField]
        private UnityEvent m_onBlinkBegin;
        [SerializeField]
        private UnityEvent m_onBlinkEnd;

        [Header("Blink Button Settings")]
        [SerializeField]
        VRBasicController.ViveButton m_blinkButton = VRBasicController.ViveButton.TOUCHPAD;

        private void Update() {
            if ( m_leftContRef.GetButtonDown((Valve.VR.EVRButtonId) m_blinkButton) || m_rightContRef.GetButtonDown((Valve.VR.EVRButtonId) m_blinkButton) ) {

                AttemptBlinkStep();
            }
        }

        private void AttemptBlinkStep() {
            CheckAreaAhead();
        }

        private void CheckAreaAhead() {
            m_onBlinkBegin.Invoke();
            Ray ray = new Ray(m_directionRef.position, m_directionRef.forward);
            RaycastHit hit;
            if ( Physics.Raycast(ray, out hit, m_blinkDistance) ) {
                CheckAreaBelow(hit.point);
            } else {
                CheckAreaBelow(ray.GetPoint(m_blinkDistance));
            }
        }

        private void CheckAreaBelow( Vector3 m_raycastPoint ) {
            Ray ray = new Ray(m_raycastPoint, Vector3.down);
            RaycastHit hit;
            if ( Physics.Raycast(ray, out hit, m_mask) ) {
                if ( hit.collider.tag == "Floor" || hit.collider.tag == "Teleportable" ) {
                    BlinkForward(hit.point);
                } else {
                    m_onBlinkEnd.Invoke();
                    Debug.Log(hit.collider.name);
                }
            }
        }

        private void BlinkForward( Vector3 a_blinkDestination ) {
            m_vrRigRef.position = a_blinkDestination;
            m_onBlinkEnd.Invoke();
        }
    }
}
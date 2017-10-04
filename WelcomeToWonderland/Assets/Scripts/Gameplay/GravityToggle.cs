using NewtonVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW {
    public class GravityToggle : MonoBehaviour {

        [Header("Gravity Objects")]
        [SerializeField]
        private List<Rigidbody> m_bodies;

        [Header("Gravity Trigger")]
        [SerializeField]
        private NVRLever m_control;

        [Header("Gravity Force Offset")]
        [SerializeField]
        private float m_minForce = 8.0f;
        [SerializeField]
        private float m_maxForce = 15.0f;

        private bool m_gravityOn = true;

        void Start() {
            SetGravity(true);
        }

        void Update() {
            if ( m_control.LeverEngaged ) {
                ToggleGravity();
            }

            // TODO: add any newly instantiated physics objects to the list.
        }

        /// <summary>
        /// This function basically toggles gravity for all objects, based on the current 
        /// gravity state.
        /// </summary>
        public void ToggleGravity() {
            float force = Random.Range(m_minForce, m_maxForce);
            m_gravityOn = !m_gravityOn;
            for ( int i = 0; i < m_bodies.Count; i++ ) {
                if ( !m_gravityOn ) {
                    NVRInteractableItem iItem = m_bodies[i].gameObject.GetComponent<NVRInteractableItem>();
                    if ( iItem != null ) iItem.EnableGravityOnDetach = false;
                    m_bodies[i].useGravity = false;
                    m_bodies[i].AddForce(Vector3.up * force * Time.deltaTime, ForceMode.VelocityChange);
                } else {
                    NVRInteractableItem iItem = m_bodies[i].gameObject.GetComponent<NVRInteractableItem>();
                    if ( iItem != null ) iItem.EnableGravityOnDetach = true;
                    m_bodies[i].useGravity = true;

                }
            }
        }

        public void SetGravity( bool a_on ) {
            float force = Random.Range(m_minForce, m_maxForce);
            for ( int i = 0; i < m_bodies.Count; i++ ) {
                if ( !a_on ) {
                    NVRInteractableItem iItem = m_bodies[i].gameObject.GetComponent<NVRInteractableItem>();
                    if ( iItem != null ) iItem.EnableGravityOnDetach = a_on;
                    m_bodies[i].useGravity = a_on;
                    m_bodies[i].AddForce(Vector3.up * force * Time.deltaTime, ForceMode.VelocityChange);
                } else {
                    NVRInteractableItem iItem = m_bodies[i].gameObject.GetComponent<NVRInteractableItem>();
                    if ( iItem != null ) iItem.EnableGravityOnDetach = a_on;
                    m_bodies[i].useGravity = a_on;

                }
            }
        }

        /// <summary>
        /// Adds a physics object to the list, as long as it isnt already in it.
        /// </summary>
        /// <param name="a_rb">The Rigidbody you wish to add</param>
        public void AddPhysicsObject( Rigidbody a_rb ) {
            if ( m_bodies.Contains(a_rb) ) return;
            m_bodies.Add(a_rb);
        }
    }
}
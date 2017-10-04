using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.AI {
    /// <summary>
    /// Author: Joel Gabriel
    /// Description: The Broken robot class is simply an 'AI' that is broken/out of control
    ///              (Easiest AI of my life! xD), it serves a comical purpose of only ever
    ///              moving forward! if it collides with a wall it bounces back slighly.
    ///              I also track its angular velocity in order to play a robot scream when
    ///              he spins our of control.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class BrokenRobot : MonoBehaviour {

        [Header("Movement Settings")]
        [SerializeField]
        private float m_speed = 15.0f;
        [SerializeField]
        private float m_bounceBackSpeed = 10.0f;

        [Header("Direction Overrides")]
        [Tooltip("Utilizes the forward vector of a custom transform defined by you.")]
        [SerializeField]
        private Transform m_newDir = null;

        [Header("Robot Sound Settings")]
        [SerializeField]
        private AudioClip m_outOfControlScream;
        [SerializeField]
        private float m_requiredSpinSpeed = 4.0f;

        private Rigidbody m_rbRef = null;
        private AudioSource m_source;
        private bool m_beingHeld = false;
        public bool BeingHeld { get { return m_beingHeld; } set { BeingHeld = value; } }

        void Start() {
            m_rbRef = GetComponent<Rigidbody>();
            m_source = GetComponent<AudioSource>();
        }

        void Update() {
            if ( Mathf.Max(Mathf.Max(m_rbRef.angularVelocity.x, m_rbRef.angularVelocity.y), m_rbRef.angularVelocity.z) >= m_requiredSpinSpeed ) {
                // SCREAM
                if ( m_source != null ) m_source.PlayOneShot(m_outOfControlScream);
            }
        }

        void FixedUpdate() {
            if ( !m_beingHeld && m_rbRef.velocity.y < 0.1f ) {
                if ( m_newDir != null ) m_rbRef.AddForce(m_newDir.forward * m_speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
                else m_rbRef.AddForce(transform.forward * m_speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
        }

        void OnCollisionEnter( Collision other ) {
            if ( m_beingHeld || m_rbRef.velocity.y > 0.1f ) return;
            if ( other.transform.tag == "Wall" ) {
                if ( m_newDir != null ) m_rbRef.AddForce(-m_newDir.forward * m_bounceBackSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
                else m_rbRef.AddForce(-transform.forward * m_bounceBackSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
            }
        }

        /// <summary>
        /// Sets the being held state of the robot
        /// </summary>
        /// <param name="a_value">the new held state</param>
        public void SetBeingHeld( bool a_value ) {
            m_beingHeld = a_value;
        }
    }
}
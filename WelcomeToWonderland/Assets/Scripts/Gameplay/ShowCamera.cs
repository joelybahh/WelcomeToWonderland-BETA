using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.AI {
    public class ShowCamera : MonoBehaviour {
        enum CAMERATYPE {
            SIMPLE, DOGDE, PHASABLE
        }

        [SerializeField]
        CAMERATYPE m_type;
        [SerializeField]
        GameObject m_player;

        Rigidbody m_rb;

        public Transform m_center;
        public Vector3 m_axis = Vector3.up;
        public Vector3 m_desiredPosition;
        public float m_radius = 5.0f;
        public float m_radiusSpeed = 0.1f;
        public float m_defaultRotationSpeed = 10.0f;
        public float m_currentRotationSpeed;


        private bool m_dodgingUp = false;
        private bool m_dodgingDown = false;

        void Start() {
            m_center = m_player.transform;
            m_rb = GetComponent<Rigidbody>();
            transform.position = ( transform.position - m_center.position ).normalized * m_radius + m_center.position;
        }

        void Update() {
            if ( !m_dodgingUp || !m_dodgingDown ) {
                transform.RotateAround(m_center.position, m_axis, m_currentRotationSpeed * Time.deltaTime);
                m_desiredPosition = ( transform.position - m_center.position ).normalized * m_radius + m_center.position;
                m_desiredPosition.y = m_center.position.y;
                transform.position = Vector3.MoveTowards(transform.position, m_desiredPosition, Time.deltaTime * m_radiusSpeed);

                m_currentRotationSpeed = m_defaultRotationSpeed;
            }
            // Go through all colliders
            // check and store the objects in an array, depending on their side of the camera
            // the one with the most (for example to the left of the object)
            // attempt an evasive maneuvor in the opposing direction 
            Collider[] hitColliders = UnityEngine.Physics.OverlapSphere(m_center.position, m_radius);
            if ( hitColliders.Length > 0 ) {
                // try and dodge up or down
                int choice = Random.Range(0, 1);
                if ( choice == 0 ) {
                    m_dodgingDown = false;
                    m_dodgingUp = true;
                } else {
                    m_dodgingUp = false;
                    m_dodgingDown = true;
                }
            }

            if ( m_dodgingUp ) {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, Time.deltaTime * m_radiusSpeed);
                StartCoroutine(StopDodgeAfterTime(1.5f, true));
            }
            if ( m_dodgingDown ) {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + -transform.up, Time.deltaTime * m_radiusSpeed);
                StartCoroutine(StopDodgeAfterTime(1.5f, false));
            }
        }

        private IEnumerator StopDodgeAfterTime( float a_time, bool a_isUp ) {
            yield return new WaitForSeconds(a_time);
            if ( a_isUp ) m_dodgingUp = false;
            else m_dodgingDown = false;
        }

        private void OnTriggerEnter( Collider other ) {
            switch ( m_type ) {
                case CAMERATYPE.SIMPLE:

                break;

                case CAMERATYPE.DOGDE:
                Vector3 direction = (other.transform.position - transform.position).normalized;
                m_desiredPosition += ( direction * 10.0f );
                transform.position = Vector3.MoveTowards(transform.position, m_desiredPosition, Time.deltaTime * m_radiusSpeed);
                break;

                case CAMERATYPE.PHASABLE:
                Destroy(other.gameObject);
                break;
            }
        }
    }
}
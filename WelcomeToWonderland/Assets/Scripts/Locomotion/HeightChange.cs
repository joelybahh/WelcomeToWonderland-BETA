using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Movement {
    public class HeightChange : MonoBehaviour {

        public Transform m_rigRef;
        
        public LayerMask m_layerMask;

        public float m_distanceThreshold;
        private float m_currentHeight;

        private Vector3 m_lastGoodStepPoint;       
        private Vector3 curPoint;

        void Start() {
           m_currentHeight = 0;
        }
        
        void Update() {
            // Get Current Height from the distance from the head to ground using a raycast
            Ray ray = new Ray(transform.GetChild(0).position, Vector3.down);
            RaycastHit hit;

            if(UnityEngine.Physics.Raycast(ray, out hit, Mathf.Infinity, m_layerMask)) {
                if (hit.transform.tag == "Teleportable") {
                    curPoint = hit.point;                  

                    if(m_lastGoodStepPoint == Vector3.zero) {
                        m_lastGoodStepPoint = hit.point;
                    }

                    // Step UP
                    if (curPoint.y > m_lastGoodStepPoint.y && (curPoint.y - m_lastGoodStepPoint.y) < m_distanceThreshold) {
                        m_lastGoodStepPoint = hit.point;
                        m_rigRef.position = new Vector3(m_rigRef.position.x, m_lastGoodStepPoint.y, m_rigRef.position.z);
                    } else if (curPoint.y < m_lastGoodStepPoint.y) {
                        m_lastGoodStepPoint = hit.point;
                        m_rigRef.position = new Vector3(m_rigRef.position.x, m_lastGoodStepPoint.y, m_rigRef.position.z);
                    }                 
                }
                
            }

            // if the CurrentHeight is greater than the raycast distance + threshold
                // Step the player up
            // else if the CurrentHeight is less than the raycast distance - threshold
                // Step the player down
        }

        /// <summary>
        /// Hard sets the current good step position to the rigs current position
        /// </summary>
        public void SetGoodstep() {
            m_lastGoodStepPoint = m_rigRef.position;
        }
    }
}

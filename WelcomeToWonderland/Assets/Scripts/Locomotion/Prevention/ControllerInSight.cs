using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Movement {
    public class ControllerInSight : MonoBehaviour {

        public Transform m_controllerRef;
        public Transform m_headsetRef;
        private SteamVR_Teleporter m_teleporterReference;
        private SteamVR_LaserPointer m_pointerReference;

        public static bool m_controllerInSight = true;

        void Start() {
            m_teleporterReference = m_controllerRef.GetComponent<SteamVR_Teleporter>();
            m_pointerReference = m_controllerRef.GetComponent<SteamVR_LaserPointer>();

        }

        void FixedUpdate() {
            Vector3 dir = m_controllerRef.position - m_headsetRef.position;
            Debug.DrawRay(m_headsetRef.position, dir);

            Ray ray = new Ray(m_headsetRef.position, dir);
            RaycastHit hit;

            if (UnityEngine.Physics.Raycast(ray, out hit)) {
                if (hit.transform.tag != "Player" && hit.transform.tag != "CigarPoint") {
                    m_controllerInSight = false;
                    m_pointerReference.enabled = false;
                    m_teleporterReference.CanTeleport = false;
                } else {
                    m_controllerInSight = true;
                    m_pointerReference.enabled = true;
                    m_teleporterReference.CanTeleport = true;
                }
            }
        }
    } 
}

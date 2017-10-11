using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Movement {
    public class HeadsetClipping : MonoBehaviour {

        public static bool m_isClipping = false;
        public SteamVR_Teleporter m_teleRef;
        public SteamVR_LaserPointer m_pointRef;
        public GameObject m_infoText;

        void OnTriggerStay(Collider a_other) {
            if (a_other.tag != "MainCamera" && a_other.tag != "Player") {
                //Debug.Log(a_other.name);
                m_isClipping = true;
                m_teleRef.CanTeleport = false;
                m_pointRef.enabled = false;
                m_infoText.SetActive (true);
            } else {
                m_isClipping = false;
                m_teleRef.CanTeleport = true;
                m_pointRef.enabled = true;
                m_infoText.SetActive (false);
            }
        }
    }
}
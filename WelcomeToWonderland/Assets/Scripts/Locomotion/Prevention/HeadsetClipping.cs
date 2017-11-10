using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Movement {
    /// <summary>
    /// Auth: Joel Gabriel
    /// Desc: Checks whether or not your headset is clipping a wall, if so, disable teleportation as 
    ///       it is likely the player is cheating.
    /// </summary>
    public class HeadsetClipping : MonoBehaviour {

        public static bool m_isClipping = false;
        public TeleportVive m_teleRef; 
        public GameObject m_infoText;

        private void OnTriggerStay(Collider a_other) {
            if (a_other.tag != "MainCamera" && 
                a_other.tag != "Player" && 
                a_other.tag != "CigarPoint" & 
                a_other.tag != "FacialFeature") 
            {
                m_isClipping = true;
                m_teleRef.CanTeleport = false;
                m_infoText.SetActive (true);
            } 
            else 
            {
                m_isClipping = false;
                m_teleRef.CanTeleport = true;
                m_infoText.SetActive (false);
            }
        }

        private void OnTriggerExit ( Collider a_other ) {
            if (a_other.tag != "MainCamera" && a_other.tag != "Player") {
                m_infoText.SetActive (false);
            }
        }
    }
}
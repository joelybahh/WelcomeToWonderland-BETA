using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

namespace WW.Movement {
    public class TeleportWithCigar : MonoBehaviour {

        [SerializeField] private Transform m_initialParent;
        [SerializeField] private Transform m_attachedParent;

        private NVRAttachJoint m_attachJoint;
        private NVRInteractableItem m_attachedItem;

        private bool m_attached = false;

        // Use this for initialization
        private void Start () {
            m_attachJoint = GetComponent<NVRAttachJoint> ();
            m_attachedItem = m_attachJoint.AttachedItem;
        }

        // Update is called once per frame
        private void Update () {
            if(m_attachedItem != null && m_attachedItem.transform.tag == "CigarPoint" && !m_attached) {
                m_attachedItem.transform.parent = m_attachedParent;
                m_attached = true;
            }

            // if it the attached boolean is true but it is not attached
            if (m_attached && m_attachedItem == null)  {
                // detach it
                m_attachedItem.transform.parent = m_initialParent;
                m_attached = false;
            }
        }
    }
}
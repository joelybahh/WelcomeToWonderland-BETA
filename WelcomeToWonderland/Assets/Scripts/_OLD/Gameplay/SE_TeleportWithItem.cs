using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class SE_TeleportWithItem : MonoBehaviour {

    [SerializeField] private NVRHand m_leftHand;
	[SerializeField] private NVRHand m_rightHand;

    private NVRInteractableItem[] m_currentlyHolding = new NVRInteractableItem[2];

    public void ParentCurrentlyHeld() {
        if (m_leftHand.CurrentlyInteracting != null) {
            m_leftHand.CurrentlyInteracting.transform.parent = m_leftHand.transform;
        }
        if (m_rightHand.CurrentlyInteracting != null) {
            m_rightHand.CurrentlyInteracting.transform.parent = m_rightHand.transform;
        }
    }

    public void UnParentCurrentlyHeld() {
        if (m_leftHand.CurrentlyInteracting != null) {
            if (m_leftHand.CurrentlyInteracting.transform.parent == m_leftHand.transform) {
                m_leftHand.CurrentlyInteracting.transform.parent = null;
            }
        }

        if (m_rightHand.CurrentlyInteracting != null) {
            if (m_rightHand.CurrentlyInteracting.transform.parent == m_rightHand.transform) {
                m_rightHand.CurrentlyInteracting.transform.parent = null;
            }
        }
    }
}

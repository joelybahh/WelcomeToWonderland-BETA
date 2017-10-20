﻿using WW.CustomPhysics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClawMachineClaw : MonoBehaviour {

    [SerializeField] private ClawMachineController m_clawMachineLever;

    [SerializeField] private int m_randResult;

    [Range(0,100)]
    [SerializeField] private int m_percentChanceOfGrab;

    private void OnTriggerEnter(Collider col) {
        m_clawMachineLever.HasItem = true;

        m_randResult = Random.Range (0, 100);

        if (m_randResult < m_percentChanceOfGrab) {
            if (m_clawMachineLever.GrabbedItem == null && col.transform.tag != "NoInteractionZone")
                m_clawMachineLever.GrabbedItem = col.GetComponent<Rigidbody> ();
        }
    }
}
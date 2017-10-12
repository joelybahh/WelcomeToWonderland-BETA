using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Joel Gabriel    
/// Desc:   This class is used to handle the entire I/O of the claw machine 
/// </summary>
public class ClawMachine : MonoBehaviour {

    private enum eMachineState {
        CONTROLLED,
        PRIZE_DROP,
        OFF
    }

    [Header ("Claw Movement Axis References")]
    [SerializeField] private Transform m_xCarriageRef;
    [SerializeField] private Transform m_zCarriageRef;
    [SerializeField] private Transform m_dropperRef;

    [Header("Claw Starting State")]
    [SerializeField] private eMachineState m_startState;

    [Header ("Claw Move Speed")]
    [Range (1, 100)]
    [SerializeField] private float m_clawSpeed;

    private eMachineState m_curState;
    
    void Start () {
        m_curState = m_startState;
    }
	
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Joel Gabriel    
/// Desc:   This class is used to handle the entire I/O of the claw machine 
/// TODO:   The use of a configurable joing
/// </summary>
public class ClawMachineLever : MonoBehaviour {

    public enum eLeverDir {
      //\\
        FORWARD,
        BACK,
        LEFT,
        RIGHT,
        FORWARD_LEFT,
        FORWARD_RIGHT,
        BACK_LEFT,
        BACK_RIGHT,
        CENTER,
        NULL
    }

    public eLeverDir curLeverDir = eLeverDir.NULL;

    [Header ("Claw Movement Axis References")]
    [SerializeField] private Transform m_xCarriageRef;
    [SerializeField] private Transform m_zCarriageRef;
    [SerializeField] private Transform m_dropperRef;

    [Header("Min & Max Settings")]
    [SerializeField] private float m_xMin;
    [SerializeField] private float m_xMax;

    [SerializeField] private float m_zMin;
    [SerializeField] private float m_zMax;

    [Header ("Claw Move Speed")]
    [SerializeField] private float m_clawSpeed;

    [SerializeField] private float m_curEulerX;
    [SerializeField] private float m_curEulerZ;

    float thresholdMin = 14f;
    float thresholdMax = 345.5f;

    void Update () {

        CheckLeverDirection ();

        #if UNITY_EDITOR
        //UpdateDeveloperOveride ();
        #endif

        UpdateCurMovement ();

        if(LeverCentered()) {
            curLeverDir = eLeverDir.CENTER;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void UpdateCurMovement () {
        switch (curLeverDir) {
            case eLeverDir.LEFT:          UpdateLeft ();     break;
            case eLeverDir.RIGHT:         UpdateRight ();    break;
            case eLeverDir.FORWARD:       UpdateForward ();  break;
            case eLeverDir.BACK:          UpdateBack ();     break;
            case eLeverDir.FORWARD_LEFT:  UpdateForward (); UpdateLeft ();       break;
            case eLeverDir.FORWARD_RIGHT: UpdateRight (); UpdateForward (); break;
            case eLeverDir.BACK_LEFT:     UpdateBack (); UpdateLeft (); break;
            case eLeverDir.BACK_RIGHT:    UpdateBack (); UpdateRight (); break;
        }
    }

    /// <summary>
    /// Function that sets the state to center
    /// </summary>
    public void StopMovement() {
        curLeverDir = eLeverDir.NULL;
    }

    private void CheckLeverDirection() {
        Vector3 eulerAngles = transform.localEulerAngles;

        m_curEulerX = eulerAngles.x;
        m_curEulerZ = eulerAngles.z;

        if (JoystickLeft()) {
            //LEFT
            curLeverDir = eLeverDir.LEFT;
        }
        if (JoystickRight()) {
            //RIGHT
            curLeverDir = eLeverDir.RIGHT;
        }
        if (JoystickForward ()) {
            //FORWARD
            curLeverDir = eLeverDir.FORWARD;
        }
        if (JoystickBack ()) {
            //BACK
            curLeverDir = eLeverDir.BACK;
        }


        //if (JoystickLeft () && JoystickForward ()) {
        //    //LEFT FORWARD
        //    curLeverDir = eLeverDir.FORWARD_LEFT;
        //}
        //if (JoystickRight () && JoystickBack ()) {
        //    //LEFT BACK
        //    curLeverDir = eLeverDir.BACK_LEFT;
        //}
        //if (JoystickForward () && JoystickForward ()) {
        //    //RIGHT FORWARD
        //    curLeverDir = eLeverDir.FORWARD_RIGHT;
        //}
        //if (JoystickBack () && JoystickBack ()) {
        //    //RIGH BACK
        //    curLeverDir = eLeverDir.BACK_RIGHT;
        //}

    }
    private bool LeverCentered() {
        if (!JoystickLeft () && !JoystickRight () && !JoystickForward () && !JoystickBack ()) {
            return true;
        } else return false;
    }

    private void UpdateLeft() {
        if (m_xCarriageRef.localPosition.x <= m_xMax)
            m_xCarriageRef.position += m_xCarriageRef.right * m_clawSpeed * Time.deltaTime; 
    }
    private void UpdateRight () {
        if (m_xCarriageRef.localPosition.x >= m_xMin)
            m_xCarriageRef.position += -m_xCarriageRef.right * m_clawSpeed * Time.deltaTime;
    }
    private void UpdateForward () {
        if (m_zCarriageRef.localPosition.z >= m_zMin)
            m_zCarriageRef.position += -m_xCarriageRef.forward * m_clawSpeed * Time.deltaTime;
    }
    private void UpdateBack () {
        if (m_zCarriageRef.localPosition.z <= m_zMax)
            m_zCarriageRef.position += m_xCarriageRef.forward * m_clawSpeed * Time.deltaTime;
    }

    private void UpdateDeveloperOveride () {
        if (Input.GetKey (KeyCode.W)) {
            curLeverDir = eLeverDir.FORWARD;
        } else if (Input.GetKey (KeyCode.A)) {
            curLeverDir = eLeverDir.LEFT;
        } else if (Input.GetKey (KeyCode.S)) {
            curLeverDir = eLeverDir.BACK;
        } else if (Input.GetKey (KeyCode.D)) {
            curLeverDir = eLeverDir.RIGHT;
        } else {
            curLeverDir = eLeverDir.CENTER;
        }
    }

    private bool JoystickLeft () {
        if ((m_curEulerZ < 350 && m_curEulerZ > 330) && curLeverDir != eLeverDir.LEFT) {
            return true;
        }
        return false;
    }
    private bool JoystickRight() {
        if (m_curEulerZ > thresholdMin && m_curEulerZ < thresholdMin + 16 && curLeverDir != eLeverDir.RIGHT) {
            return true;
        }
        return false;
    }
    private bool JoystickForward () {
        if ((m_curEulerX < thresholdMax && m_curEulerX > 330) && curLeverDir != eLeverDir.FORWARD) {
            return true;
        }
        return false;
    }
    private bool JoystickBack () {
        if (m_curEulerX > thresholdMin && m_curEulerX < thresholdMin + 16 && curLeverDir != eLeverDir.BACK) {
            return true;
        }
        return false;
    }
}

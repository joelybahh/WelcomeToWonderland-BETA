using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    float threshold = 14f;

    void Update () {

        CheckLeverDirection ();

        #if UNITY_EDITOR
        UpdateDeveloperOveride ();
        #endif

        UpdateCurMovement ();
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
        //z,x

        Vector3 eulerAngles = transform.localEulerAngles;

        Debug.Log (eulerAngles + "Z");
        if(transform.localEulerAngles.z < -threshold) {
            //LEFT
            curLeverDir = eLeverDir.LEFT;
        }
        if (transform.localEulerAngles.z > threshold) {
            //RIGHT
            curLeverDir = eLeverDir.RIGHT;
        }

        if (transform.localEulerAngles.x < -threshold) {
            //FORWARD
            curLeverDir = eLeverDir.FORWARD;
        }
        if (transform.localEulerAngles.x > threshold) {
            //BACK
            curLeverDir = eLeverDir.BACK;
        }

        // TODO: FIX INSPECTOR BEING A SHITBAG
        //float AngleAboutY ( Transform obj ) {
        //    Vector3 objFwd = obj.forward;
        //    float angle = Vector3.Angle (objFwd, Vector3.forward);
        //    float sign = Mathf.Sign (Vector3.Cross (objFwd, Vector3.forward).y);
        //    return angle * sign;
        //}


        //TODO: FORWARD_LEFT, FORWARD_RIGHT
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
}

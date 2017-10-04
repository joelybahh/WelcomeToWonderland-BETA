using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_PuzzleEyeScanner : SE_PuzzleEvent {
    [Header("Particle System Reference")]
    [SerializeField] private ParticleSystem m_eyeScanner;

    [Header("Animator Settings")]
    [SerializeField] private Animator m_controller;

    [Header("Transform References")]
    [SerializeField] private Transform m_headTransform;
    [SerializeField] private Transform m_scannerTransform;

    [Header("Holographic Display Settings")]
    [SerializeField] private GameObject m_eyeScanePhaseOne;
    [SerializeField] private GameObject m_eyeScanePhaseTwo;
    [SerializeField] private GameObject m_eyeScanePhaseThree;
    [SerializeField] private GameObject m_accessGranted;
    [SerializeField] private GameObject m_accessDenied;

    private AudioClip m_announcement;
    private bool m_hasCompleted = false;
    [SerializeField] private float m_scanTimer = 0;
    private Animator m_unfoldController;

    bool m_isInUnfoldZone = false;

    void Start() {
        m_unfoldController = GetComponent<Animator>();
    }

    public override void CompleteEvent( ) {
        base.CompleteEvent();
        //SE_PuzzleEventHandler.Instance.Announcer.AddAnnouncementToQueue(m_announcement);
    }


    private void OnTriggerEnter(Collider other) {
        m_eyeScanner.Play();
        m_unfoldController.ResetTrigger("Refold");
        m_unfoldController.SetTrigger("Unfold");
        m_isInUnfoldZone = true;
        if (other.tag == "EyeKey" || other.tag == "Player")
        {
            if (IsLookingAtScanner(m_headTransform, m_scannerTransform))
            {

            }
            else
            {
                Debug.Log("In scan zone, but facing the wrong way");
            }
        }
    }

    private void OnTriggerStay(Collider other)  {
        if (IsLookingAtScanner(m_headTransform, m_scannerTransform))
        {
            if(other.tag != "EyeKey" && other.tag != "Player") {
                return;
            }

            m_scanTimer += Time.deltaTime;

            #region hide your eyes

            if(m_scanTimer < 1.0f) {
                ResetAllPhaseTextExceptOne(0);
            } else if(m_scanTimer >= 1.0 && m_scanTimer < 2.0f) {
                ResetAllPhaseTextExceptOne(1);
            } else if(m_scanTimer >= 2.0f && m_scanTimer < 3.0f) {
                ResetAllPhaseTextExceptOne(2);
            }

            #endregion

            if (m_scanTimer > 3.0f) {
                if (other.tag == "EyeKey") {
                    m_completed = true;
                    m_controller.SetTrigger("doorOpen");
                    GrantAccess(true);
                    CompleteEvent();
                }  else if(other.tag == "Player") {
                    m_completed = false;
                    GrantAccess(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        m_eyeScanner.Stop();

        if (other.tag == "EyeKey" || other.tag == "Player") {
            m_scanTimer = 0.0f;
            m_unfoldController.ResetTrigger("Unfold");
            m_unfoldController.SetTrigger("Refold");
            m_isInUnfoldZone = false;
            m_eyeScanePhaseOne.SetActive(false);
            m_eyeScanePhaseTwo.SetActive(false);
            m_eyeScanePhaseThree.SetActive(false);
            m_accessDenied.SetActive(false);
            m_accessGranted.SetActive(false);
        }
    }

    private bool IsLookingAtScanner(Transform a_HmdLookDir, Transform a_EyeScannerLookDir) {

        if (a_HmdLookDir == null || a_EyeScannerLookDir == null) return false;

        float dot = Vector3.Dot(
            a_HmdLookDir.forward, 
            (a_EyeScannerLookDir.position - a_HmdLookDir.position).normalized
            );

        if (dot > 0.7f) {   // Fairly close to looking directly at it.
            return true;
        }

        return false;
    }

    void ResetAllPhaseTextExceptOne(int a_index) {
        switch (a_index) {
            case 0: { m_eyeScanePhaseOne.SetActive(true); m_eyeScanePhaseTwo.SetActive(false); m_eyeScanePhaseThree.SetActive(false); break; }
            case 1: { m_eyeScanePhaseOne.SetActive(false); m_eyeScanePhaseTwo.SetActive(true); m_eyeScanePhaseThree.SetActive(false); break; }
            case 2: { m_eyeScanePhaseOne.SetActive(false); m_eyeScanePhaseTwo.SetActive(false); m_eyeScanePhaseThree.SetActive(true); break; }
        }
    }

    void GrantAccess(bool a_state) {
        if(a_state) {
            m_accessDenied.SetActive(false);
            m_accessGranted.SetActive(true);
        } else {
            m_accessGranted.SetActive(false);
            m_accessDenied.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Puzzles {
    public class TutorialRoom : Puzzle {

        [Header("Wall Rigidbody References")]
        [SerializeField] private Rigidbody m_wallFrontRB;
        [SerializeField] private Rigidbody m_wallLeftRB;
        [SerializeField] private Rigidbody m_wallRightRB;

        [Header("Wall Push Direction References")]
        [SerializeField] private Transform m_wallFrontPushPos;
        [SerializeField] private Transform m_wallLeftPushPos;
        [SerializeField] private Transform m_wallRightPushPos;

        [Header("Push Forces")]
        [SerializeField] private float m_wallPushForce = 40;

        [Header("Temporary Puzzle Solve")]
        [SerializeField] private NewtonVR.NVRButton m_triggerZone;

        private bool m_doOnce = true;
        private bool m_hasWaitedDelayTime;

        private bool m_hasTeleportGlove = false;
        public bool HasTeleportGlove {
            get { return m_hasTeleportGlove; }
            set { m_hasTeleportGlove = value; }
        }

        void Start() {
            StartCoroutine(WaitTime(5));
        }

        void Update() {
            

            if ( m_triggerZone.ButtonWasPushed && m_hasWaitedDelayTime) {
                Debug.Log("happened");
                HasTeleportGlove = true;
            }
        }

        void FixedUpdate() {
            if (!m_doOnce) return;
            if (m_doOnce) {
                if (HasTeleportGlove) {

                    SetupWallRigidbodies();


                    Ray frontRay = new Ray(m_wallFrontPushPos.position, m_wallFrontPushPos.forward);
                    Ray leftRay = new Ray(m_wallLeftPushPos.position, m_wallLeftPushPos.forward);
                    Ray rightRay = new Ray(m_wallRightPushPos.position, m_wallRightPushPos.forward);

                    RaycastHit hit;

                    if (UnityEngine.Physics.Raycast(frontRay, out hit, Mathf.Infinity)) {
                        m_wallFrontRB.AddForceAtPosition(m_wallRightPushPos.forward * m_wallPushForce * Time.fixedDeltaTime, hit.point);
                    }
                    if (UnityEngine.Physics.Raycast(leftRay, out hit, Mathf.Infinity)) {
                        m_wallLeftRB.AddForceAtPosition(m_wallLeftPushPos.forward * m_wallPushForce * Time.fixedDeltaTime, hit.point);
                    }
                    if (UnityEngine.Physics.Raycast(rightRay, out hit, Mathf.Infinity)) {
                        m_wallRightRB.AddForceAtPosition(m_wallRightPushPos.forward * m_wallPushForce * Time.fixedDeltaTime, hit.point);
                    }

                    m_doOnce = false;
                    CompletePuzzle();
                }
            }
        }

        private void SetupWallRigidbodies() {
            m_wallFrontRB.isKinematic = false;
            m_wallFrontRB.useGravity = true;

            m_wallLeftRB.isKinematic = false;
            m_wallLeftRB.useGravity = true;

            m_wallRightRB.isKinematic = false;
            m_wallRightRB.useGravity = true;
        }

        private IEnumerator WaitTime(float a_seconds) {
            yield return new WaitForSeconds(a_seconds);
            m_hasWaitedDelayTime = true;
        }

        public void Puzzletrigger()
        {

            
            CompletePuzzle();

        }

        public void DropWalls()
        {
            SetupWallRigidbodies();


            Ray frontRay = new Ray(m_wallFrontPushPos.position, m_wallFrontPushPos.forward);
            Ray leftRay = new Ray(m_wallLeftPushPos.position, m_wallLeftPushPos.forward);
            Ray rightRay = new Ray(m_wallRightPushPos.position, m_wallRightPushPos.forward);

            RaycastHit hit;

            if (UnityEngine.Physics.Raycast(frontRay, out hit, Mathf.Infinity))
            {
                m_wallFrontRB.AddForceAtPosition(m_wallRightPushPos.forward * m_wallPushForce * Time.fixedDeltaTime, hit.point);
            }
            if (UnityEngine.Physics.Raycast(leftRay, out hit, Mathf.Infinity))
            {
                m_wallLeftRB.AddForceAtPosition(m_wallLeftPushPos.forward * m_wallPushForce * Time.fixedDeltaTime, hit.point);
            }
            if (UnityEngine.Physics.Raycast(rightRay, out hit, Mathf.Infinity))
            {
                m_wallRightRB.AddForceAtPosition(m_wallRightPushPos.forward * m_wallPushForce * Time.fixedDeltaTime, hit.point);
            }

            m_doOnce = false;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace WW.CustomPhysics {
    /// <summary>
    /// Author: Joel Gabriel
    /// Desc: This class is used for handling the swapping out between damaged states of objects. 
    /// It is setup in a dynamic fashion, allowing an endless amount of 'damaged state prefabs',
    /// and an easy way to update them.
    /// </summary>
    public class BreakableItem : MonoBehaviour {
        #region Serialized Variables
        [Header ("Impact Force Settings")]
        [SerializeField] private float m_requiredImpactForce;

        [Header ("SERVICE ROBOT SETTINGS")]
        [SerializeField] private bool m_isServiceRobot = false;

        #endregion

        #region Private Variables

        private List<GameObject> m_brokenPrefabs;
        private NavMeshAgent m_agent;
        private int m_currentIndex = 0;

        #endregion

        #region Unity Methods

        private void Start () {
            // Initialize the list of broken objects
            m_brokenPrefabs = new List<GameObject> ();

            // Get the reference to the nav mesh agent
            m_agent = GetComponent<NavMeshAgent> ();

            // Loop though children and add the objects to the list.
            for (int i = 0; i < transform.childCount; i++) {
                m_brokenPrefabs.Add (transform.GetChild (i).gameObject);
            }
        }
      
        private void OnCollisionEnter ( Collision collision ) {
           // Debug.Log (collision.relativeVelocity.magnitude);
            
            if ((m_agent != null && !m_agent.enabled) && m_isServiceRobot) {
                if (collision.relativeVelocity.magnitude >= m_requiredImpactForce) {
                    GoToNextDamageState ();
                }
            } else {
                if (collision.relativeVelocity.magnitude >= m_requiredImpactForce) {
                    GoToNextDamageState ();
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Goes to the next 'damage' state, and updates the art asset accordingly 
        /// </summary>
        private void GoToNextDamageState () {
            m_currentIndex++;

            // Check we are at an index that doesn't exeed the count.
            if (m_currentIndex > m_brokenPrefabs.Count - 1) {
                // TODO: DESTROY GAME OBJECT, TRIGGER PARTICLE EFFECT?
                return;
            }

            // Set the new state to active
            m_brokenPrefabs[m_currentIndex].SetActive (true);

            // Loop through and set the old one to inactive
            for (int i = 0; i < m_brokenPrefabs.Count; i++) {
                if (i != m_currentIndex) {
                    m_brokenPrefabs[i].SetActive (false);
                }
            }
        }

        #endregion
    }
}
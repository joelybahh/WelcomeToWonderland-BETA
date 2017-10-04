using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW.Physics;

namespace WW.Helpers {
    /// <summary>
    /// Desc: Optimises the gumball physics by looping through one every frame and setting them to kinematic 
    ///       if they have been at rest for 3 seconds, and the valve is shut, if the valve is open, turn kinematic
    ///       back to false, thus reducing physics calculation. Done in a corouting to stagger looping.
    /// Author: Joel Gabriel
    /// </summary>
    public class OptimiseGumballs : MonoBehaviour {

        #region Serialized Variables

        [SerializeField] private List<Rigidbody> m_gumballs;

        [Tooltip("The lower the substep, the more the loop is spread out across multiple frames")]
        [SerializeField] private int m_substep = 10;

        [SerializeField] private float m_kinematicWaitTime = 1.5f;

        #endregion

        #region Private Variables

        private GumballDispenser m_dispenser;
        private bool hasSetKinematic = false;

        #endregion

        #region Unity Methods

        private void Start() {
            m_dispenser = GetComponent<GumballDispenser>();
            StartCoroutine("UpdateGumballs");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates gumball kinematic state based on the state of the twister
        /// </summary>
        /// <returns>null</returns>
        private IEnumerator UpdateGumballs() {
            while (true) {
                while (m_dispenser.m_twistState == eTwistState.CLOSED && !hasSetKinematic) {
                    yield return new WaitForSeconds(m_kinematicWaitTime);

                    for (int i = 0; i < m_gumballs.Count; i++) {
                        m_gumballs[i].isKinematic = true;
                        m_gumballs[i].useGravity = false;
                        if (i % m_substep == 0) yield return null;
                    }
                    hasSetKinematic = true;
                }

                while (m_dispenser.m_twistState == eTwistState.OPENED && hasSetKinematic) {
                    for (int i = 0; i < m_gumballs.Count; i++) {
                        m_gumballs[i].isKinematic = false;
                        m_gumballs[i].useGravity = true;
                        if ( i % m_substep == 0 ) yield return null;
                    }
                    hasSetKinematic = false;
                    yield return null;
                }
                yield return null;
            }           
        }
        #endregion
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace WW.AI {
    /// <summary>
    /// Author: Joel Gabriel
    /// Description: he AI for the water robot that patrols the intermission room
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class WaterRobot : MonoBehaviour {

        [SerializeField]
        public enum ePathingBehaviour {
            LOOPING,
            PING_PONG,
            STOP_AT_END
        }

        //public PowerOutletSwitch m_switch;

        #region Serialized Variables

        [Header("Movement Settings")]
        [SerializeField]
        float m_moveSpeed = 2.0f;
        [SerializeField]
        float m_nextNodeDelay = 0.0f;

        [Header("Pathfinding Behaviour Settings")]
        [SerializeField]
        ePathingBehaviour m_pathingBehaviour = ePathingBehaviour.LOOPING;

        [Header("AI Manager Override")]
        [SerializeField]
        AIPathManager m_pathOverride;

        #endregion

        #region Private Variables

        private NavMeshAgent      m_agent           = null;
        private Transform         m_currentTarget   = null;
        private AIPathManager  m_path            = null;
        private int               m_pathIndex       = 0;
        private bool              m_movingForward   = true;
        private bool              m_goalNodeReached = false;
        private bool              m_isStoppedAtEnd  = false;

        #endregion

        void Start() {
            m_agent = GetComponent<NavMeshAgent>();

            if ( m_pathOverride == null ) m_path = GameObject.Find("MainWaypoints").GetComponent<AIPathManager>(); // FIXME: SLOW, pass by reference prefered!
            else m_path = m_pathOverride;

            m_currentTarget = m_path.GetNearestWaypoint(transform.position, out m_pathIndex);
            m_agent.SetDestination(m_currentTarget.position);
            m_agent.speed = m_moveSpeed;
        }

        void Update() {
            //if (m_switch.m_switchState == true) {
            //    m_agent.enabled = true;
            //} else m_agent.enabled = false;

            if ( m_isStoppedAtEnd ) return;
            switch ( m_pathingBehaviour ) {
                case ePathingBehaviour.LOOPING: UpdateLooping(); break;
                case ePathingBehaviour.PING_PONG: UpdatePingPong(); break;
                case ePathingBehaviour.STOP_AT_END: UpdateStopAtEnd(); break;
            }
        }

        #region Private Methods

        /// <summary>
        /// Updates the logic for LOOPING behaviour
        /// </summary>
        private void UpdateLooping() {
            if ( Vector3.Distance(transform.position, m_agent.destination) <= 1 ) {
                if ( m_nextNodeDelay <= 0 ) {
                    Vector3 newTarget = m_path.GetNextWaypoint(ref m_pathIndex, m_pathingBehaviour, ref m_movingForward).position;
                    m_agent.SetDestination(newTarget);
                } else {
                    DelayUpdatingWaypoint();
                }
            }
        }

        /// <summary>
        /// Updates the logic for PING_PONG behaviour
        /// </summary>
        private void UpdatePingPong() {
            if ( Vector3.Distance(transform.position, m_agent.destination) <= 1 ) {
                if ( m_nextNodeDelay <= 0 ) {
                    Vector3 newTarget = m_path.GetNextWaypoint(ref m_pathIndex, m_pathingBehaviour, ref m_movingForward).position;
                    m_agent.SetDestination(newTarget);
                } else {
                    DelayUpdatingWaypoint();
                }
            }
        }

        /// <summary>
        /// Updates the logic for STOP_AT_END behaviour
        /// </summary>
        private void UpdateStopAtEnd() {
            // if we have no current destination          
            if ( Vector3.Distance(transform.position, m_agent.destination) <= 1 ) {
                if ( m_nextNodeDelay <= 0 ) {
                    // get a reference to the next waypoint
                    Transform newTarget = m_path.GetNextWaypoint(ref m_pathIndex, m_pathingBehaviour, ref m_movingForward);
                    // if that reference happens to be null, we reached the end, so STOP_AT_END and return
                    if ( newTarget == null ) { return; m_isStoppedAtEnd = true; }
                    // otherwise simply setup its new destination
                    else {
                        m_agent.SetDestination(newTarget.position);
                    }
                } else {
                    DelayUpdatingWaypoint(true);
                }
            }
        }

        /// <summary>
        /// This coroutine simply delays the time between moving on to the next waypoint.
        /// </summary>
        /// <param name="a_isStopAtEnd">whether or not you are in the StopAtEnd behaviour</param>
        /// <returns></returns>
        private IEnumerator DelayUpdatingWaypoint( bool a_isStopAtEnd = false ) {
            if ( !a_isStopAtEnd ) {
                yield return new WaitForSeconds(m_nextNodeDelay);
                Vector3 newTarget = m_path.GetNextWaypoint(ref m_pathIndex, m_pathingBehaviour, ref m_movingForward).position;
                m_agent.SetDestination(newTarget);
            } else {
                yield return new WaitForSeconds(m_nextNodeDelay);
                Transform newTarget = m_path.GetNextWaypoint(ref m_pathIndex, m_pathingBehaviour, ref m_movingForward);
                if ( newTarget == null ) yield return null;
                // otherwise simply setup its new destination
                else {
                    m_agent.SetDestination(newTarget.position);
                }
            }
        }

        /// <summary>
        /// re-enables the ai after a certain amount of time
        /// </summary>
        /// <param name="a_timer">time (in seconds) to wait</param>
        /// <returns></returns>
        private IEnumerator ReEnableTimer( float a_timer ) {
            yield return new WaitForSeconds(a_timer);
            GetComponent<NavMeshAgent>().enabled = true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Simply restarts our AI if it has reached the stop point (fyi, it only will work on STOP_AT_END behaviour.)
        /// </summary>
        public void ResetStoppedAI() {
            if ( !m_isStoppedAtEnd ) return;    // clearly the robot isnt off, dont continue with this function call

            m_isStoppedAtEnd = false;
            m_agent.SetDestination(m_path.GetWaypointPositionFromIndex(1));
        }

        /// <summary>
        /// Sets the AI to a desired state
        /// </summary>
        /// <param name="a_state">The off/on state</param>
        public void SetAI( bool a_state ) {
            GetComponent<NavMeshAgent>().enabled = a_state;
        }

        /// <summary>
        /// Enables the ai after 'x' amount of seconds
        /// </summary>
        /// <param name="a_time">Time in seconds to enable</param>
        public void EnableAiAfterTime( float a_time ) {
            StartCoroutine(ReEnableTimer(a_time));
        }

        #endregion
    }
}
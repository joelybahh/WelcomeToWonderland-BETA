using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.AI {
    /// <summary>
    /// Author: Joel Gabriel
    /// Description: Handles the logic for the AI Waypoint system.
    ///              Setup to decouple the waypoints from the AI
    ///              agents themselves.
    /// </summary>
    public class AIPathManager : MonoBehaviour {
        [Header("Pathfinding Waypoints")]
        [SerializeField]
        private List<Transform> m_waypoints;

        public Transform GetNearestWaypoint( Vector3 a_agentPos, out int a_pathIndex ) {
            Transform nearestTarget = null;
            a_pathIndex = -1;
            float curShortestDistance = 0.0f;
            for ( int i = 0; i < m_waypoints.Count; i++ ) {
                if ( i == 0 ) {
                    nearestTarget = m_waypoints[i];
                    curShortestDistance = Vector3.Distance(m_waypoints[i].position, a_agentPos);
                } else {
                    if ( Vector3.Distance(m_waypoints[i].position, a_agentPos) < curShortestDistance ) {
                        curShortestDistance = Vector3.Distance(m_waypoints[i].position, a_agentPos);
                        nearestTarget = m_waypoints[i];
                    }
                }
            }

            for ( int i = 0; i < m_waypoints.Count; i++ ) {
                if ( m_waypoints[i] == nearestTarget ) {
                    a_pathIndex = i;
                }
            }

            return nearestTarget;
        }

        public Transform GetNextWaypoint( ref int a_currentTargetIndex, WaterRobot.ePathingBehaviour a_behaviour, ref bool a_movingForward ) {
            switch ( a_behaviour ) {
                case WaterRobot.ePathingBehaviour.LOOPING: {
                    a_movingForward = true;

                    if ( m_waypoints.Count - 1 == a_currentTargetIndex ) a_currentTargetIndex = 0;
                    else a_currentTargetIndex++;

                    return m_waypoints[a_currentTargetIndex];
                }
                case WaterRobot.ePathingBehaviour.PING_PONG: {
                    // if you are at the end waypoint and are currently moving in a forward direction
                    if ( m_waypoints.Count - 1 == a_currentTargetIndex && a_movingForward ) {
                        a_currentTargetIndex--;
                        a_movingForward = false;
                        // else if we are simply just moving forward
                    } else if ( a_movingForward == true ) {
                        a_currentTargetIndex++;
                        a_movingForward = true;
                        // else if we are moving backwards, and are currently at the beginning node
                    } else if ( a_movingForward == false && a_currentTargetIndex == 0 ) {
                        a_currentTargetIndex++;
                        a_movingForward = true;
                        // Otherwise we are merely moving backwards
                    } else {
                        a_currentTargetIndex--;
                        a_movingForward = false;
                    }
                    // now return what the resulting path is
                    return m_waypoints[a_currentTargetIndex];
                }
                case WaterRobot.ePathingBehaviour.STOP_AT_END: {
                    a_movingForward = true;
                    if ( m_waypoints.Count - 1 == a_currentTargetIndex ) return null;
                    else return m_waypoints[a_currentTargetIndex++];
                }
                default: { a_movingForward = true; return null; }
            }
        }

        public Vector3 GetWaypointPositionFromIndex( int a_index ) {
            return m_waypoints[a_index].position;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {   // visualize the patrol path of the enemy 
        private void OnDrawGizmos()
        {
            const float waypointGizmoRadius = 0.3f;
            // iterate through all waypoints of PathPatrol component in Unity
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                // draw a waypoint in Unity
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                // draw lines between waypoints
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }
        // getting the index of waypoints of patrol path
        public int GetNextIndex(int i)
        {
            // when the last waypoint is patrolled - return to the first one (line 28)
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}

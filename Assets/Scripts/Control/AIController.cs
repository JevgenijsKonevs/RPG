﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 3f;

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;
        Vector3 guardPosition;
        
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;
        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            // find the object location with the tag "Player", which is assigned to Player model
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
            // guards starting position
            guardPosition = transform.position;
            // time since enemy saw a player
            
        }
        private void Update()
        {
            if (health.IsDead()) return;
            // if player is in the area of chase then trigger chase action
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            // suspicion mode
            else if (timeSinceLastSawPlayer < suspicionTime) 
            {
                SuspicionBehaviour();
            }
            // player is out of attack range and is not returning
            else
            {
                // return to the guarding position
                PatrolBehaviour();
            }
            // update the time
            timeSinceLastSawPlayer += Time.deltaTime;
        }
        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }
         private void SuspicionBehaviour()
        {
            GetComponent<ActionSchedule>().CancelCurrentAction();
        }
         private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            mover.StartMoveAction(nextPosition);
        }
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }
        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private bool InAttackRangeOfPlayer()
        {
            // calculate distance between player and enemy
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }
        
        // Called by Unity. To show what is the chase distance of enemy
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            // draw a blue sphere, when the enemy is selected
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

    }
}



using System.Collections;
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

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;
        Vector3 guardPosition;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            // find the object location with the tag "Player", which is assigned to Player model
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
            // guards starting position
            guardPosition = transform.position;
        }
        private void Update()
        {
            if (health.IsDead()) return;
            // if player is in the area of chase then trigger chase action
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                // return to the guarding position
                mover.StartMoveAction(guardPosition);
            }
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



using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {   // adding fields for adjusting the figher`s combat qualities
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        Transform target;
        float timeSinceLastAttack = 0;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;

            if (!GetIsInRange())
            { // if the player is out of range then move to attack target
                GetComponent<Mover>().MoveTo(target.position);

            }
            else
            { // When the player is in range of attack then he stops moving and starts attacking 
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }

        }
        // function to start the attack
        private static void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }

        }

        // function to calculate and compare the distance value from player`s position, target`s position and the range of weapon
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionSchedule>().StartAction(this);
            target = combatTarget.transform;

        }
        public void Cancel()
        {
            target = null;
        }
        // Animation Event (fix for Unity error)
        void Hit()
        {

        }
    }
}
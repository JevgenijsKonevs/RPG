using UnityEngine;
using RPG.Movement;
using RPG.Core;


namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {   // adding fields for adjusting the figher`s combat qualities
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            // if target is dead then stop any actions
            if (target.IsDead()) return;
            if (!GetIsInRange())
            { // if the player is out of range then move to attack target
                // 1f means going to full speed
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);

            }
            else
            { // When the player is in range of attack then he stops moving and starts attacking 
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }

        }
        // function to start the attack
        private void AttackBehaviour()
        {
            // look at the enemy. Rotate vector 
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {   // This will trigger Hit()
               TriggerAttack();
                timeSinceLastAttack = 0;

            }

        }
        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }
        // Animation Event (fix for Unity error)
        void Hit()
        {
            if (target == null) {
                return;
            }
            // dealing damage to target
            target.TakeDamage(weaponDamage);
        }

        // function to calculate and compare the distance value from player`s position, target`s position and the range of weapon
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionSchedule>().StartAction(this);
            target = combatTarget.GetComponent<Health>();

        }
        public void Cancel()
        {
            StopAttack();
            target = null;
        }
        // Stop the attack after the movement
        public void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
        
        // fix to check if it is possible to attack the enemy
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest!=null &&!targetToTest.IsDead();
        }

    }
}
using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {   // implementing Health field for enemy and setting it to 100
        [SerializeField] float healthPoints = 100f;
        // setting the state of the enemy
        bool isDead = false;
        public bool IsDead()
        {
            return isDead;
        }
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();

            }

        }
        // implementing the function which will trigger die animation 
        private void Die()
        {

            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionSchedule>().CancelCurrentAction();
        }
        public object CaptureState()
        {
            return healthPoints;
        }
        public void RestoreState(object state)
        {
            healthPoints = (float) state;
              if (healthPoints == 0)
            {
                Die();

            }
            
        }
    }
}
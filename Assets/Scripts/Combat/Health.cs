using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
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
        }
    }
}
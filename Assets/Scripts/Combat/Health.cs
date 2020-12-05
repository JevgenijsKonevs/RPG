using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {   // implementing Health field for enemy and setting it to 100
        [SerializeField] float health = 100f;
        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            print(health);
        }
    }
}
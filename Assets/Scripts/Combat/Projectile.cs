using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;


namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = true;
        Health target = null;
        float damage = 0;
        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;
            if (isHoming && !target.IsDead())
            {
                 // point the projectile to the targets position
            transform.LookAt(GetAimLocation());
            }
            // move the projectile with the given speed to the targets position
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }
        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) return target.transform.position;
            // return targets position + half ot the targets height
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target)
            {
                return;
            }
            if (target.IsDead()) return;
            target.TakeDamage(damage);
            // destroy the arrow that was shot
            Destroy(gameObject);
        }
    }
}


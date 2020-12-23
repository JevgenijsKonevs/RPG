using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Transform target = null;
        [SerializeField] float speed = 1f;


        // Update is called once per frame
        void Update()
        {
            if (target == null) return;
            // point the projectile to the targets position
            transform.LookAt(GetAimLocation());
            // move the projectile with the given speed to the targets position
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) return target.position;
            // return targets position + half ot the targets height
            return target.position + Vector3.up * targetCapsule.height / 2;
        }
    }
}


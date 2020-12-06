using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement {
    public class Mover : MonoBehaviour, IAction
{
    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;
    Health health;
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
    }
    void Update()
    {
        // enable navMeshAgent only if enemy is not dead. Stuck in the dead body fix
        navMeshAgent.enabled = !health.IsDead();
        UpdateAnimator();
    }
    public void StartMoveAction(Vector3 destination)
    {
        GetComponent<ActionSchedule>().StartAction(this);
        MoveTo(destination);
    }
    private void MoveToCursor() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit =  Physics.Raycast(ray, out hit);
        if (hasHit) {
            MoveTo(hit.point);
        }
    }
    public void MoveTo(Vector3 destination)
    {
         navMeshAgent.destination = destination;
         navMeshAgent.isStopped = false;
    }
    public void Cancel()
    {
        navMeshAgent.isStopped = true;
    }

    private void UpdateAnimator() 
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed",speed);
    }
}
}

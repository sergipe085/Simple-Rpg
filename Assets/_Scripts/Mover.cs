using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [Header("--- CORE COMPONENTS ---")]
    private NavMeshAgent agent = null;
    private Animator animator = null;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            MoveToCursor();
        }
        UpdateAnimator();
        UpdateRotation();
    }

    private void MoveToCursor() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue);

        NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, float.MaxValue, NavMesh.AllAreas);
        agent.SetDestination(navHit.position);
    }

    private void UpdateAnimator() {
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        float speedPercentage = speed / agent.speed;
        animator.SetFloat("ForwardSpeed", speedPercentage);
    }

    private void UpdateRotation() {
        Vector3 direction = agent.desiredVelocity.normalized;
        transform.forward = Vector3.Lerp(transform.forward, direction, 10f * Time.deltaTime);
    }
}

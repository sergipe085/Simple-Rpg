using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [Header("--- CORE COMPONENTS ---")]
        private NavMeshAgent agent = null;
        private Animator animator = null;
        private ActionScheduler actionScheduler = null;

        private void Awake() {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update() {
            UpdateAnimator();
            UpdateRotation();
        }

        public void StartMoveAction(Vector3 destination) {
            actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination) {
            NavMesh.SamplePosition(destination, out NavMeshHit navHit, float.MaxValue, NavMesh.AllAreas);
            agent.SetDestination(navHit.position);
            agent.isStopped = false;
        }

        public void Stop() {
            agent.isStopped = true;
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

            if (direction == Vector3.zero) return;

            transform.forward = Vector3.Lerp(transform.forward, direction, 10f * Time.deltaTime);
        }

        public void Cancel() {
            Stop();
        }
    }
}

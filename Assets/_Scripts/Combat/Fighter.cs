using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [Header("--- CORE COMPONENTS ---")]
        private Mover mover = null;
        private ActionScheduler actionScheduler = null;
        private Animator animator = null;

        [Header("--- CONFIGURATIONS ---")]
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBeetwenAttacks = 1.0f;

        private Transform target = null;
        private float attackTimer = 0.0f;

        private void Awake() {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        private void Update() {
            attackTimer += Time.deltaTime;

            if (!target) return;

            if (!GetIsInRange()) {
                actionScheduler.StartAction(this);
                mover.MoveTo(target.position);
            }
            else {
                mover.Stop();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour() {
            if (attackTimer >= timeBeetwenAttacks) {
                attackTimer = 0f;
                animator.SetTrigger("Attack");
            }
        }

        private bool GetIsInRange() {
            return Vector3.Distance(transform.position, target.position) <= weaponRange;
        }

        public void Attack(CombatTarget combatTarget) {
            target = combatTarget.transform;
        }

        public void Cancel() {
            target = null;
        }

        //Animation event
        private void Hit() {

        }
    }
}

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

        private Health target = null;
        private float attackTimer = 0.0f;

        private void Awake() {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        private void Update() {
            attackTimer += Time.deltaTime;

            if (!target) return;

            if (target.IsDead()) {
                target = null;
                return;
            }

            if (!GetIsInRange()) {
                mover.MoveTo(target.transform.position);
            }
            else {
                mover.Stop();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour() {
            LookToTarget();
            if (attackTimer >= timeBeetwenAttacks) {
                attackTimer = 0f;
                animator.SetTrigger(Settings.attack);
                animator.ResetTrigger(Settings.stopAttack);
            }
        }

        private bool GetIsInRange() {
            return Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
        }

        public bool CanAttack(CombatTarget combatTarget) {
            if (!combatTarget) return false;
            
            Health combatTargetHealth = combatTarget.GetComponent<Health>();
            
            return !combatTargetHealth.IsDead();
        }

        public void Attack(CombatTarget combatTarget) {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel() {
            target = null;
            animator.SetTrigger(Settings.stopAttack);
            animator.ResetTrigger(Settings.attack);
        }

        //Animation event
        private void Hit() {
            if (!target) return;

            target.TakeDamage(10);
        }

        private void LookToTarget() {
            if (!target) return;

            Vector3 direction = (target.transform.position - transform.position).normalized;
            direction.y = 0f;
            
            transform.forward = Vector3.Lerp(transform.forward, direction, 20f * Time.deltaTime);
        }
    }
}

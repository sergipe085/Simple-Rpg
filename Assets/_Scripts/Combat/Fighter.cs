using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [Header("--- CORE COMPONENTS ---")]
        private Mover mover = null;

        [Header("--- CONFIGURATIONS ---")]
        [SerializeField] private float weaponRange = 2f;

        private Transform target = null;

        private void Awake() {
            mover = GetComponent<Mover>();
        }

        private void Update() {
            if (!target) return;

            if (!GetIsInRange()) {
                mover.MoveTo(target.position);
            }
            else {
                mover.Stop();
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour
    {
        [Header("--- CORE COMPONENTS ---")]
        private Mover mover = null;
        private Fighter fighter = null;
        [SerializeField] private LayerMask groundLayer;

        private void Awake() {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        private void Update() {
            if (HandleCombat()) return;
            if (HandleMovement()) return;
        }

        private bool HandleCombat() {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits) {
                if (hit.transform.TryGetComponent<CombatTarget>(out CombatTarget combatTarget)) {
                    if (!fighter.CanAttack(combatTarget)) continue;
                    
                    if (Input.GetMouseButtonDown(0)) {
                        fighter.Attack(combatTarget);
                    }
                    return true;
                }
            }

            return false;
        }

        private bool HandleMovement()
        {
            if (!Physics.Raycast(GetMouseRay(), out RaycastHit hit, float.MaxValue, groundLayer)) return false;

            if (Input.GetMouseButton(0)) {
                mover.StartMoveAction(hit.point);
            }

            return true;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour
    {
        [Header("--- CORE COMPONENTS ---")]
        private Mover mover = null;

        private void Awake() {
            mover = GetComponent<Mover>();
        }

        private void Update() {
            if (Input.GetMouseButton(0)) {
                HandleMovement();
            }
        }

        private void HandleMovement() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit, float.MaxValue);

            mover.MoveTo(hit.point);
        }
    }
}

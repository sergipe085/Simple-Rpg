using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSpawner : MonoBehaviour
{
    [SerializeField] private Ragdoll ragdollPrefab = null;
    [SerializeField] private Transform originalRootBone = null;

    private Health health = null;

    private void Awake() {
        health = GetComponent<Health>();

        health.OnDieEvent += Health_OnDieEvent;
    }

    private void Health_OnDieEvent() {
        Ragdoll ragdoll = Instantiate(ragdollPrefab, transform.position, transform.rotation);
        ragdoll.Setup(originalRootBone);
        Destroy(this.gameObject);
    }
}

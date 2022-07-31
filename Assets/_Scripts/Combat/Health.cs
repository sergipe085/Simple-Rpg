using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    private bool isDead = false;

    public event Action OnDieEvent = null;

    public bool IsDead() {
        return isDead;
    }

    public void TakeDamage(float damage) {        
        health = Mathf.Max(health - damage, 0);

        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        if (isDead) return;

        OnDieEvent?.Invoke();
        isDead = true;
    }
}

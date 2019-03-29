using UnityEngine;

public class LivingEntity : MonoBehaviour {

    public float maxHealth = 100f;
    public float health;

    // Events
    public delegate void DeathEventHandler();
    public event DeathEventHandler DeathEvent;
    public delegate void HitEventHandler();
    public event HitEventHandler HitEvent;

    void Awake() {
        health = maxHealth;
    }

    public void TakeDamage(float damage) {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        OnHitEvent();


        if (health <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }

    public void OnDeathEvent() {
        if (DeathEvent != null) {
            DeathEvent.Invoke();
        }
    }

    public void OnHitEvent() {
        if (HitEvent != null) {
            HitEvent.Invoke();
        }
    }
}

#pragma warning disable 0649
using UnityEngine;

public class Projectile : MonoBehaviour {

    private float projectileDamage = 20f;
    private float projectileSpeed = 70f;
    private float projectileLifetime = 5f;
    private LayerMask projectileHitMask;

    void Update() {
        MoveProjectile();
        CollisionCheck();
        ProjectileAging();
    }

    void MoveProjectile() {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    void CollisionCheck() {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, .65f, projectileHitMask, QueryTriggerInteraction.Collide)) {
            ProjectileHit(hit);
        }
    }

    void ProjectileHit(RaycastHit hit) {
        // Apply damage to the object that we hit ?
        LivingEntity le = hit.collider.GetComponent<LivingEntity>();
        if (le != null) {
            le.TakeDamage(projectileDamage);
            SoundSystem.PlaySound("impact_flesh01", transform.position);
        }

        // Audio
        SoundSystem.PlaySound("impact_flesh01", transform.position);

        KillProjectile();
    }

    void ProjectileAging() {
        projectileLifetime -= Time.deltaTime;
        if (projectileLifetime <= 0) {
            KillProjectile();
        }
    }

    void KillProjectile() {
        Destroy(gameObject);
    }

    public void SetupProjectile(float projectileDamage, float projectileSpeed, float projectileLifetime, LayerMask projectileHitMask) {
        this.projectileDamage = projectileDamage;
        this.projectileSpeed = projectileSpeed;
        this.projectileHitMask = projectileHitMask;
        this.projectileLifetime = projectileLifetime;
    }

}

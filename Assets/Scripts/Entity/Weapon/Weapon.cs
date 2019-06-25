#pragma warning disable 0649
using UnityEngine;

public class Weapon : MonoBehaviour {

    [Header("Weapon Stats")]
    [SerializeField] private float damage = 20f;
    [SerializeField] private float rateOfFire = 666f;

    [Header("Projectile Settings")]
    [SerializeField] private bool isHitScan = true;
    [SerializeField] private Projectile projectile;
    [SerializeField] private float projectileRange = 50f;
    [SerializeField] private float projectileSpeed = 70f;
    [SerializeField] private Vector3 projectileSpawnOffset;
    [SerializeField] private LayerMask hitMask;

    [Header("Visuals")]
    public Vector3 weaponViewModelOffset = new Vector3(0, 0, 0);

    [Header("FX")]
    [SerializeField] private Transform shootVFX;
    [SerializeField] private Transform impactFleshVFX;
    [SerializeField] private Transform impactDefaultVFX;

    private float nextPossibleShootTime;
    private float lastAttackId = 0;

    public void Shoot() {
        if (!CanShoot) {
            return;
        }

        Vector3 projectilePredictionVector = GameManager.LocalPlayer.Player_Controller.velocity.normalized *
                                    GameManager.LocalPlayer.Player_Controller.moveSpeed * Time.deltaTime;
        Vector3 projectileSpawnPosition = transform.position + projectilePredictionVector + transform.TransformDirection(projectileSpawnOffset);

        // Hitscan weapon
        if (isHitScan) {
            Ray ray = new Ray(GameManager.LocalPlayer.Player_Camera.MainCamera.transform.position, GameManager.LocalPlayer.Player_Camera.MainCamera.transform.forward);
            RaycastHit hit;
            LivingEntity le = null;
            if (Physics.Raycast(ray, out hit, projectileRange, hitMask, QueryTriggerInteraction.Collide)) {
                le = hit.collider.GetComponent<LivingEntity>();
                // If we hit a living entity, apply damage and spawn a dummy projectile
                if (le != null) {
                    le.TakeDamage(damage);
                }

                // Impact audio
                SoundSystem.PlaySound("impact_flesh01", hit.point);

                // Impact fx
                Transform impactVfx = le == null ? impactDefaultVFX : impactFleshVFX;
                if (impactVfx != null)
                    Instantiate(impactVfx, hit.point, Quaternion.identity);
            }
            
            Projectile dummyClone = Instantiate(projectile, projectileSpawnPosition, 
            GameManager.LocalPlayer.Player_Camera.MainCamera.transform.rotation);
            dummyClone.SetupProjectile(projectileSpeed, hit.collider != null ? hit.point : GameManager.LocalPlayer.transform.forward * projectileRange);
        }

        // Projectile weapon
        else {
            // Spawn projectile
            Projectile clone = Instantiate(projectile, projectileSpawnPosition, 
                            GameManager.LocalPlayer.Player_Camera.MainCamera.transform.rotation);
            clone.SetupProjectile(damage, projectileSpeed, projectileRange, hitMask);
        }

        // Firerate. Calculate when we can shoot next 
        nextPossibleShootTime = Time.time + (60 / rateOfFire);

        // Play shooting animation
        float rng = lastAttackId;
        while (rng == lastAttackId) {
            rng = (int)Random.Range(0, 3);
        }
        lastAttackId = rng;
        GameManager.LocalPlayer.Player_ViewModelAnimator.PlayAction(rng);

        // Play shooting sound
        SoundSystem.PlaySound2D("blaster_shoot01");

        // Shooting VFX
        if (shootVFX != null) {
            Transform vfxClone = Instantiate(shootVFX, transform.position + transform.TransformDirection(projectileSpawnOffset), 
                                    GameManager.LocalPlayer.Player_Camera.MainCamera.transform.rotation);
            vfxClone.SetParent(transform);
        }
    }

    bool CanShoot {
        get {
            bool b = true;
            // Falsifiers
            if (Time.time < nextPossibleShootTime) {
                b = false;
            }
            return b;
        }
    }

}
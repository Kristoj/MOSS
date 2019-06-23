﻿#pragma warning disable 0649
using UnityEngine;

public class Weapon : MonoBehaviour {

    [Header("Weapon Stats")]
    [SerializeField] private float damage = 20f;
    [SerializeField] private float rateOfFire = 666f;

    [Header("Projectile Settings")]
    [SerializeField] private Projectile projectile;
    [SerializeField] private float projectileSpeed = 70f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private Vector3 projectileSpawnOffset;
    [SerializeField] private LayerMask hitMask;

    [Header("FX")]
    [SerializeField] private Transform shootVFX;

    private float nextPossibleShootTime;
    private float lastAttackId = 0;

    public void Shoot() {
        if (!CanShoot) {
            return;
        }

        // Spawn projectile
        Vector3 projectileSpawnPosition = transform.position + transform.TransformDirection(projectileSpawnOffset);
        Projectile clone = Instantiate(projectile, projectileSpawnPosition, 
                           GameManager.LocalPlayer.Player_Camera.MainCamera.transform.rotation);
        clone.SetupProjectile(damage, projectileSpeed, projectileLifetime, hitMask);

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
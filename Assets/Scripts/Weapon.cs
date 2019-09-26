using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float secondsBetweenShots = 1f;
    [SerializeField] TextMeshProUGUI ammoText;
    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true; // TODO Allows user to fire, switch weapon and switch back, and fire again regardless of secondsBetweenShots
    }

    void Update()
    {
        UpdateAmmoUI();
        if (Input.GetButtonDown("Fire1") && ammoSlot.GetAmmoAmount(ammoType) > 0)
        {
            StartCoroutine(Shoot());
        }
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = ammoSlot.GetAmmoAmount(ammoType).ToString();
    }

    private IEnumerator Shoot()
    {
        if (canShoot)
        {
            ammoSlot.ReduceAmmoAmount(ammoType);
            PlayMuzzleFlash();
            ProcessRaycast();
            canShoot = false;
            yield return new WaitForSeconds(secondsBetweenShots);
            canShoot = true;
        }        
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) { return; }
            target.TakeDamage(damage);
        }
        else { return; }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        var hitEffectFX = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitEffectFX, 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

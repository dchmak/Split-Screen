/*
* Created by Daniel Mak
*/

using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class ShootingComponent : MonoBehaviour {

    [Header("Statistcs")]
    public float chargeTime = 1f;
    public float range = 25f;
    public float shakiness = 0.05f;
    public float beamDuration = 0.2f;
    public float damage = 1f;

    [Header("Components")]
    public ParticleSystem laserChargingParticle;
    public ParticleSystem laserParticle;

    private CameraShake shaker;
    private ParticleSystem.MainModule laserMainModule;
    private ParticleSystem.MainModule laserChargingMainModule;

    public void Shoot(Vector3 direction) {
        StartCoroutine(ShootCoroutine(direction));
    }

    private IEnumerator ShootCoroutine(Vector3 direction) {
        Assert.AreEqual(0, direction.z);

        float angle = Vector2.SignedAngle(direction, Vector2.right) * Mathf.Deg2Rad;

        // charge
        print("Charging...");        
        laserChargingMainModule.startRotation = angle;
        laserChargingParticle.Play();

        float timer = 0;
        while (timer < chargeTime) {

            timer += Time.deltaTime;
            yield return null;
        }

        // shoot
        timer = 0f;
        while (timer < beamDuration) {
            // ray cast to see if hit
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, ~(1 << gameObject.layer));
            if (hit) {
                print("Hit " + hit.collider.name);
                hit.transform.GetComponent<Health>().TakeDamage(damage);
            }

            // show laser beam
            print("Fire!");
            shaker.CameraShaker(beamDuration, shakiness, Time.deltaTime);
            laserMainModule.startRotation = angle;
            laserParticle.Play();

            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void Awake() {
        shaker = GetComponentInChildren<CameraShake>();

        laserChargingMainModule = laserChargingParticle.main;
        laserMainModule = laserParticle.main;

        laserChargingMainModule.duration = chargeTime;
        laserChargingMainModule.startLifetime = chargeTime;

        laserMainModule.duration = beamDuration;
        laserMainModule.startLifetime = beamDuration;
    }

    private void OnValidate() {
        if (chargeTime < 0) chargeTime = 0;
        if (range < 0) range = 0;
        if (shakiness < 0) shakiness = 0;
        if (beamDuration < 0) beamDuration = 0;
        if (damage < 0) damage = 0;
    }
}
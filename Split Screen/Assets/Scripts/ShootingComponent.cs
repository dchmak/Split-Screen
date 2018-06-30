/*
* Created by Daniel Mak
*/

using System.Collections;
using UnityEngine;

public class ShootingComponent : MonoBehaviour {

    public LayerMask ignoreMask;
    public float chargeTime = 1f;
    public float shakiness = 0.05f;
    public float shakeDuration = 0.2f;
    public ParticleSystem laserChargingParticle;

    private CameraShake shaker;

    public void Shoot(Vector3 direction, float maxDistance) {
        StartCoroutine(ShootCoroutine(direction, maxDistance));
    }

    private IEnumerator ShootCoroutine(Vector3 direction, float maxDistance) {

        // charge
        print("Charging...");

        ParticleSystem.MainModule main = laserChargingParticle.main;
        //laserParticle.startRotation = 
        main.duration = chargeTime;
        main.startLifetime = chargeTime;
        main.startRotation = Vector2.SignedAngle(direction, Vector2.right) * Mathf.Deg2Rad;
        laserChargingParticle.Play();

        float timer = 0;
        while (timer < chargeTime) {

            timer += Time.deltaTime;
            yield return null;
        }

        // fire laser
        print("Fire!");

        shaker.CameraShaker(main.duration / 5f, shakiness, Time.deltaTime);

        // ray cast to see if hit
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, ~ (1 << gameObject.layer));
        if (hit) {
            print("Hit " + hit.collider.name);
        }
    }

    private void Start() {
        laserChargingParticle = GetComponentInChildren<ParticleSystem>();
        shaker = GetComponentInChildren<CameraShake>();
    }

    private void OnValidate() {
        if (chargeTime < 0) chargeTime = 0;
        if (shakiness < 0) shakiness = 0;
        if (shakeDuration < 0) shakeDuration = 0;
    }
}
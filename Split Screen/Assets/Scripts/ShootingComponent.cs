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
    public LineRenderer laserLine;

    private CameraShake shaker;

    public void Shoot(Vector3 direction) {
        StartCoroutine(ShootCoroutine(direction));
    }

    private IEnumerator ShootCoroutine(Vector3 direction) {
        Assert.AreEqual(0, direction.z);

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

        timer = 0f;
        laserLine.enabled = true;
        while (timer < beamDuration) {
            // ray cast to see if hit
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, ~(1 << gameObject.layer));
            if (hit) {
                print("Hit " + hit.collider.name);
            }

            // show laser beam
            print("Fire!");

            shaker.CameraShaker(beamDuration, shakiness, Time.deltaTime);
            laserLine.SetPosition(0, transform.position);
            laserLine.SetPosition(1, hit ? hit.transform.position : transform.position + direction * range);

            timer += Time.deltaTime;
            yield return null;
        }
        laserLine.enabled = false;
    }

    private void Start() {
        shaker = GetComponentInChildren<CameraShake>();
    }

    private void OnValidate() {
        if (chargeTime < 0) chargeTime = 0;
        if (range < 0) range = 0;
        if (shakiness < 0) shakiness = 0;
        if (beamDuration < 0) beamDuration = 0;
        if (damage < 0) damage = 0;
    }
}
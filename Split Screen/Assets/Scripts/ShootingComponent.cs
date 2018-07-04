/*
* Created by Daniel Mak
*/

using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class ShootingComponent : MonoBehaviour {
    
    public float shakiness = 0.05f;
    public LaserBeamController laser;

    private CameraShake shaker;

    public void Shoot(Vector3 direction) {
        StartCoroutine(ShootCoroutine(direction));
    }

    private IEnumerator ShootCoroutine(Vector3 direction) {
        if (laser.IsPlaying()) yield break;

        Assert.AreEqual(0, direction.z);
        
        float angle = Vector2.SignedAngle(direction, Vector2.right);

        laser.PlayBeam(angle);

        // charge
        print("Charging...");
        yield return new WaitForSeconds(laser.chargeTime);

        // shoot
        print("Fire!");
        shaker.CameraShaker(laser.beamDuration, shakiness, Time.deltaTime);

        float timer = 0f;
        while (timer < laser.beamDuration) {
            // ray cast to see if hit
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, laser.range, ~(1 << gameObject.layer));
            if (hit) {
                print("Hit " + hit.collider.name);
                hit.transform.GetComponent<Health>().TakeDamage(laser.damage);
            }
            
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void Awake() {
        shaker = GetComponentInChildren<CameraShake>();
    }

    private void OnValidate() {
        if (shakiness < 0) shakiness = 0;
    }
}
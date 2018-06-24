/*
* Created by Daniel Mak
*/

using UnityEngine;

public class ShootingComponent : MonoBehaviour {

    public LayerMask ignoreMask;

	public void Shoot(Vector3 direction, float maxDistance) { 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, ~ (1 << gameObject.layer));
        if (hit) {
            print("Hit " + hit.collider.name);
        }
    }
}
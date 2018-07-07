/*
* Created by Daniel Mak
*/

using UnityEngine;

public class Player : MonoBehaviour {

    [HideInInspector] public Movement movement;
    [HideInInspector] public ShootingComponent shoot;
    [HideInInspector] public HealthSystem health;

	private void Awake () {
        movement = GetComponent<Movement>();
        shoot = GetComponent<ShootingComponent>();
        health = GetComponent<HealthSystem>();
	}
}
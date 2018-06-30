/*
* Created by Daniel Mak
*/

using System.Collections;
using UnityEditor;
using UnityEngine;

public enum InputMode { Keyboard, Controller };

public class Movement : MonoBehaviour {

    public InputMode mode;
    public float normalSpeed = 15f;
    public float blinkSpeed = 30f;
    public float blinkTime = 0.5f;
    public GameObject blinkEffect;
    public float shakiness = 0.05f;

    private Vector3 direction;
    private string horizontalAxis, verticalAxis, blinkButton, fireButton;
    private bool blink;
    private float blinkTimer;
    private CameraShake shaker;
    private Vector3 camOriginalPosition;
    private ShootingComponent shooter;

    private void Start () {
        horizontalAxis = mode.ToString() + " Horizontal";
        verticalAxis = mode.ToString() + " Vertical";
        blinkButton = mode.ToString() + " Blink";
        fireButton = mode.ToString() + " Fire";

        blink = false;
        blinkTimer = 0f;

        shaker = GetComponentInChildren<CameraShake>();
        shooter = GetComponent<ShootingComponent>();
    }
	
	private void Update () {
        if (mode == InputMode.Controller) {
            if (Input.GetButtonDown(blinkButton) && !blink) {
                print("Blink!");

                // blink particle effect
                if (blinkEffect != null) {
                    GameObject particle = Instantiate(blinkEffect, transform.position, Quaternion.identity);
                    ParticleSystem system = particle.GetComponent<ParticleSystem>();
                    if (system != null) Destroy(particle, system.main.duration + system.main.startLifetime.Evaluate(0));

                    shaker.CameraShaker(system.main.duration, shakiness, Time.deltaTime);
                }

                blink = true;
            }
        } else {

        }

        direction = new Vector3(Input.GetAxisRaw(horizontalAxis), Input.GetAxisRaw(verticalAxis), 0).normalized;
        Vector3 velocity = (blink && blinkTimer < blinkTime) ? direction * blinkSpeed * Time.deltaTime : direction * normalSpeed * Time.deltaTime;
        transform.position += velocity;

        Debug.DrawRay(transform.position, direction, Color.red);
        if (Input.GetButtonDown(fireButton)) {
            print("Shoot!");

            shooter.Shoot(direction, 100f);
         }

        if (blink) {
            blinkTimer += Time.deltaTime;
        }

        if (blinkTimer > blinkTime) {
            blink = false;
            blinkTimer = 0f;
        }
    }

    private void OnValidate() {
        name = mode.ToString() + " Player";

        if (normalSpeed < 0) normalSpeed = 0;
        if (blinkSpeed < normalSpeed) blinkSpeed = normalSpeed;
        if (blinkTime < 0) blinkTime = 0;
        if (shakiness < 0) shakiness = 0;
    }
}
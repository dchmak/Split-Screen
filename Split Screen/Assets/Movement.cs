/*
* Created by Daniel Mak
*/

using UnityEngine;

public enum InputMode { Keyboard, Controller };

public class Movement : MonoBehaviour {

    public InputMode mode;
    public float normalSpeed = 15f;
    public float blinkSpeed = 30f;
    public float blinkTime = 0.5f;
    public GameObject blinkEffect;
    public float shakiness = 3f;

    private Vector3 direction;
    private string horizontalAxis, verticalAxis;
    private bool blink;
    private float blinkTimer;
    private Camera cam;
    private Vector3 camOriginalPosition;

    private void Start () {
        horizontalAxis = mode.ToString() + " Horizontal";
        verticalAxis = mode.ToString() + " Vertical";

        blink = false;
        blinkTimer = 0f;

        cam = GetComponentInChildren<Camera>();
    }
	
	private void Update () {
        if (mode == InputMode.Controller) {
            if (Input.GetButtonDown("Boost") && !blink) {
                print("Blink!");

                // blink particle effect
                GameObject particle = Instantiate(blinkEffect, transform.position, Quaternion.identity);
                ParticleSystem system = particle.GetComponent<ParticleSystem>();
                if (system != null) Destroy(particle, system.main.duration + system.main.startLifetime.Evaluate(0));

                camOriginalPosition = cam.transform.localPosition;

                blink = true;
            }
        } else {

        }

        direction = new Vector3(Input.GetAxisRaw(horizontalAxis), Input.GetAxisRaw(verticalAxis), 0).normalized;
        Vector3 velocity = (blink && blinkTimer < blinkTime) ? direction * blinkSpeed * Time.deltaTime : direction * normalSpeed * Time.deltaTime;
        transform.position += velocity;


        if (blink) {

            // cmaera shake
            cam.transform.localPosition += new Vector3(Random.Range(-1f, 1f) * shakiness, Random.Range(-1f, 1f) * shakiness, 0);

            blinkTimer += Time.deltaTime;
        }

        if (blinkTimer > blinkTime) {
            cam.transform.localPosition = camOriginalPosition;

            blink = false;
            blinkTimer = 0f;

        }
    }
}
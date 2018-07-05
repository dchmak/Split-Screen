/*
* Created by Daniel Mak
*/

using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public float size = 10f;
    public ParticleSystem[] glyphs;
    public ParticleSystem[] addons;

    private GameManager gameManager;
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;

        ParticleSystem.MainModule main = GetComponent<ParticleSystem>().main;
        Gizmos.DrawWireSphere(transform.position, size);
    }

    private void Start() {
        gameManager = GameManager.instance;
        gameManager.ReadyToStartEvent += OnReadyToStart;
    }

    private void OnValidate() {
        foreach (ParticleSystem glyph in glyphs) {
            ParticleSystem.MainModule main = glyph.main;
            main.startSize = size * 4f;
        }

        foreach (ParticleSystem addon in addons) {
            ParticleSystem.ShapeModule shape = addon.shape;
            shape.radius = size;
        }
    }

    private void OnReadyToStart() {
        foreach (ParticleSystem glyph in glyphs) {
            glyph.Stop();
        }

        foreach (ParticleSystem addon in addons) {
            addon.Stop();
        }
    }
}
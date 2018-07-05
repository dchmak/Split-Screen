/*
* Created by Daniel Mak
*/

using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public float size = 10f;
    public ParticleSystem[] glyphs;
    public ParticleSystem[] needles;

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

        foreach (ParticleSystem needle in needles) {
            ParticleSystem.ShapeModule shape = needle.shape;
            shape.radius = size;
        }
    }

    private void OnReadyToStart() {
        gameObject.SetActive(false);
    }
}
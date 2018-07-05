/*
* Created by Daniel Mak
*/

using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public float size = 10f;
    public ParticleSystem[] glyphs;
    public ParticleSystem[] needles;
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;

        ParticleSystem.MainModule main = GetComponent<ParticleSystem>().main;
        Gizmos.DrawWireSphere(transform.position, size);
    }

    private void OnValidate() {
        tag = "Spawn";

        foreach (ParticleSystem glyph in glyphs) {
            ParticleSystem.MainModule main = glyph.main;
            main.startSize = size;
        }

        foreach (ParticleSystem needle in needles) {
            ParticleSystem.ShapeModule shape = needle.shape;
            shape.radius = size / 4f;
        }
    }
}
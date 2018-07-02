/*
* Created by Daniel Mak
*/

using UnityEngine;
using UnityEngine.Assertions;

public class Health : MonoBehaviour {

    public float maxHealth = 100f;

    [ReadOnly] [SerializeField] private float health;

    public void TakeDamage(float damage) {
        Assert.IsTrue(damage > 0);

        health -= damage;
    }

    public void Heal(float heal) {
        Assert.IsTrue(heal > 0);

        health += heal;
    }

    private void Start () {
        health = maxHealth;
    }

    private void OnValidate() {
        if (maxHealth < 0) maxHealth = 0f;
    }
}
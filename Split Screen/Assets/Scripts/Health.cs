/*
* Created by Daniel Mak
*/

using UnityEngine;
using UnityEngine.Assertions;

public class Health : MonoBehaviour {

    public float maxHealth = 100f;
    [ReadOnly] [SerializeField] private float health;

    [Space]
    public Bar healthBar;

    public void TakeDamage(float damage) {
        Assert.IsTrue(damage > 0);

        health -= damage;
        if (health < 0f) health = 0f;
    }

    public void Heal(float heal) {
        Assert.IsTrue(heal > 0);

        health += heal;
        if (health > maxHealth) health = maxHealth;
    }

    private void Start () {
        health = maxHealth;
    }

    private void LateUpdate() {
        healthBar.SetValue(health / maxHealth);
    }

    private void OnValidate() {
        if (maxHealth < 0) maxHealth = 0f;
        if (healthBar != null) healthBar.name = name + " Health Bar";
    }
}
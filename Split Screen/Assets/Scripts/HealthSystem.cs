/*
* Created by Daniel Mak
*/

using UnityEngine;
using UnityEngine.Assertions;

public class HealthSystem : MonoBehaviour {

    public float maxHealth = 100f;
    [ReadOnly] [SerializeField] private float health;

    [Space]
    public Bar healthBar;

    private GameManager gameManager;

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

    public bool IsDead() {
        return health <= 0;
    }

    private void Awake() {
        health = maxHealth;
    }

    private void Start () {
        gameManager = GameManager.instance;        
        gameManager.ReadyToStartEvent += OnReadyToStart;
    }

    private void LateUpdate() {
        healthBar.SetValue(health / maxHealth);
    }

    private void OnValidate() {
        if (maxHealth < 0) maxHealth = 0f;
        if (healthBar != null) healthBar.name = name + " Health Bar";
    }

    private void OnReadyToStart() {
        healthBar.gameObject.SetActive(true);
    }
}
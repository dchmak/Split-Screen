/*
* Created by Daniel Mak
*/

using UnityEngine;

[RequireComponent(typeof(Player))]
public class PowerUpComponent : MonoBehaviour {

    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    public void PowerUp() {
        player.health.Heal(10);
    }
}
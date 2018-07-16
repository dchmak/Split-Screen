/*
* Created by Daniel Mak
*/

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            print(collision.name + "picked up an item.");
        }        
    }
}
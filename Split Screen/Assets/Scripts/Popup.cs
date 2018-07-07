/*
* Created by Daniel Mak
*/

using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour {

    private TextMeshProUGUI textGUI;
    private Animator animator;

    public void Display(string textToDisplay) {
        textGUI.text = textToDisplay;
        animator.Play("Popup");
    }

    private void Awake() {
        textGUI = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }
}
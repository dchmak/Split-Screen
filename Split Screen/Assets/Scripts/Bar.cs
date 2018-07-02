/*
* Created by Daniel Mak
*/

using UnityEngine;

[ExecuteInEditMode]
public class Bar : MonoBehaviour {

    [Header("Settings")]
    public Vector2 dimension;
    public Vector2 border;

    [Header("Components")]
    public GameObject background;
    public GameObject fill;
    public GameObject icon;

    private void Awake() {
        background.GetComponent<RectTransform>().sizeDelta = dimension;
        fill.GetComponent<RectTransform>().sizeDelta = dimension - 2 * border;
    }
}
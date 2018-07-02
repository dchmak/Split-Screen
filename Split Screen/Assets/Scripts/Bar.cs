/*
* Created by Daniel Mak
*/

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Bar : MonoBehaviour {

    [Header("Settings")]
    public Vector2 dimension;
    public Vector2 borderSize;
    public Vector2 iconSize;
    [Space]
    public Color borderColor;
    public Color fillColor;
    [Space]
    public Sprite iconSprite;

    [Header("Components")]
    public GameObject background;
    public GameObject fill;
    public GameObject icon;

    public void SetValue(float value) {
        Assert.IsTrue(0 <= value && value <= 1);

        fill.GetComponent<Image>().fillAmount = value;
    }

    private void Update() {
        #if UNITY_EDITOR
            background.GetComponent<RectTransform>().sizeDelta = dimension;
            background.GetComponent<Image>().color = borderColor;

            fill.GetComponent<RectTransform>().sizeDelta = dimension - 2 * borderSize;
            fill.GetComponent<Image>().color = fillColor;

            icon.SetActive(iconSprite != null);
            icon.GetComponent<Image>().sprite = iconSprite;
            icon.GetComponent<RectTransform>().sizeDelta = iconSize;
            icon.GetComponent<RectTransform>().localPosition = new Vector3(dimension.x / 2f, -iconSize.y * 1.2f, 0);
        #endif
    }
}
/*
* Created by Daniel Mak
*/

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour {

    public float smoothTime = 0.1f;

    private Camera cam;
    private string includedTag = "Player";
    private GameObject[] included;
    private float minZoom;

    private void Awake () {
        cam = GetComponent<Camera>();

        included = GameObject.FindGameObjectsWithTag(includedTag);

        minZoom = transform.parent.GetComponentInChildren<LaserBeamController>().range * 1.2f;
	}
	
	private void Update () {
        Bounds bounds = new Bounds(transform.position, Vector3.zero);
        foreach (GameObject obj in included) {
            bounds.Encapsulate(obj.transform.position);
        }

        float vel = 0f;
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, Mathf.Max(Mathf.Max(bounds.size.x, bounds.size.y) * 1.2f, minZoom), ref vel, smoothTime);
	}
}
/*
 * Create by Daniel Mak
 * [TagSelector] attribute is created by brechtos - http://www.brechtos.com/tagselectorattribute/
 */

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AutoFocusCamera: MonoBehaviour {
    
    public Vector3 offset;
    public float minZoom;
    public float maxZoom;
    public float zoomLimit;
    public float smoothTime;
    [TagSelector] public string targetTag;
    [TagSelector] public string includedTag;

    private GameObject target;
    private GameObject[] included;
    private Vector3 velocity;
    private Camera cam;

    public static bool needUpdate;

    private void Start() {
        if (targetTag != TagSelectorAttribute.noTagSelectedString) {
            print(targetTag);
            target = GameObject.FindGameObjectWithTag(targetTag);
        }

        if (includedTag != TagSelectorAttribute.noTagSelectedString) {
            print(includedTag);
            included = GameObject.FindGameObjectsWithTag(includedTag);
        }

        cam = GetComponent<Camera>();
    }

    private void Update() {
        if (needUpdate) {
            if (includedTag != TagSelectorAttribute.noTagSelectedString) {
                print(includedTag);
                included = GameObject.FindGameObjectsWithTag(includedTag);
            } else {
                included = null;
            }
        }
    }

    private void LateUpdate() {
        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + offset, ref velocity, smoothTime);

        Bounds bounds = new Bounds(center: target == null ? transform.position : target.transform.position, size: Vector3.zero);
        if (target != null) bounds.Encapsulate(target.transform.position);

        if (included != null) {
            foreach (GameObject gameObject in included) {
                bounds.Encapsulate(gameObject.transform.position);
            }
        }

        //cam.fieldOfView = Mathf.Max(bounds.size.x, bounds.size.y);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, Mathf.Lerp(maxZoom, minZoom, Mathf.Sqrt(Mathf.Pow(bounds.size.x, 2) + Mathf.Pow(bounds.size.y, 2)) / zoomLimit), Time.deltaTime);
    }

    private void OnValidate() {
        name = "Auto Focus Camera";
    }
}

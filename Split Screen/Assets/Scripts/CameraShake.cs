/*
* Created by Daniel Mak
*/

using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public void CameraShaker(float time, float shakiness, float interval) {
        StartCoroutine(CameraShakerCoroutine(time, shakiness, interval));
    }

	private IEnumerator CameraShakerCoroutine(float time, float shakiness, float interval) {
        float timer = 0f;

        Vector3 camOriginalPosition = transform.localPosition;

        while (timer < time) {
            transform.localPosition += new Vector3(Random.Range(-1f, 1f) * shakiness, Random.Range(-1f, 1f) * shakiness, 0);

            yield return new WaitForSeconds(interval);
            timer += interval;
        }

        transform.localPosition = camOriginalPosition;
    }
}
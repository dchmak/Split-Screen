/*
* Created by Daniel Mak
*/

using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	public IEnumerator CameraShaker(float time, float shakiness, float interval) {
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
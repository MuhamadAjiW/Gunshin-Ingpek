using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public void SmoothLookAt(GameObject target, float durationMiliSeconds = 100f)
    {
        StartCoroutine(SmoothLookAtAsync(target, durationMiliSeconds / 1000));
    }

    IEnumerator SmoothLookAtAsync(GameObject target, float durationSeconds)
    {
        Vector3 relativePosition = target.transform.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(relativePosition);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.time * 1f);
        Quaternion currentRotation = transform.rotation;

        float counter = 0;
        while (counter < durationSeconds)
        {
            counter += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(currentRotation, newRotation, EaseInOut(counter / durationSeconds));
            yield return null;
        }
    }

    private float EaseInOut(float relativeTime)
    {
        return Mathf.Lerp(relativeTime * relativeTime, 1 - ((1 - relativeTime) * (1 - relativeTime)), relativeTime);
    }
}

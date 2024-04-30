using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public void SmoothLookAt(GameObject targetDirection, GameObject targetPosition, float durationMiliSeconds = 0f)
    {
        if (durationMiliSeconds == 0f)
        {
            Vector3 relativeFinalPosition = targetDirection.transform.position - targetPosition.transform.position;
            Quaternion newRotation = Quaternion.LookRotation(relativeFinalPosition);
            transform.rotation = newRotation;
            transform.position = targetPosition.transform.position;
        }
        else
        {
            StartCoroutine(SmoothLookAtAsync(targetDirection, targetPosition, durationMiliSeconds / 1000));
        }
    }

    IEnumerator SmoothLookAtAsync(GameObject targetDirection, GameObject targetPosition, float durationSeconds)
    {
        Vector3 originalPosition = transform.position;
        Vector3 relativeFinalPosition = targetDirection.transform.position - targetPosition.transform.position;
        Quaternion newRotation = Quaternion.LookRotation(relativeFinalPosition);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.time * 1f);
        Quaternion currentRotation = transform.rotation;

        float counter = 0;
        while (counter < durationSeconds)
        {
            counter += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(currentRotation, newRotation, EaseInOut(counter / durationSeconds));
            transform.position = Vector3.Lerp(originalPosition, targetPosition.transform.position, EaseInOut(counter / durationSeconds));
            yield return null;
        }
    }

    private float EaseInOut(float relativeTime)
    {
        return Mathf.Lerp(relativeTime * relativeTime, 1 - ((1 - relativeTime) * (1 - relativeTime)), relativeTime);
    }
}

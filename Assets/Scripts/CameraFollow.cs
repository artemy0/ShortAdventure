using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    [Range(0f, 1f)]
    public float SmoothSpeed = 0.125f;
    public Vector3 Offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition = Target.position + Offset;
        Vector3 smoothedPossition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime * 40f);
        transform.position = smoothedPossition;
    }
}

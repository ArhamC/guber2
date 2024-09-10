using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public Transform target;       // Reference to the car's transform
    public float smoothSpeed = 0.125f;  // Speed at which the camera follows the car
    public Vector3 offset;         // Offset of the camera from the car

    void LateUpdate()
    {
        if (target != null)
        {
            // Desired position of the camera
            Vector3 desiredPosition = target.position + offset;
            // Smoothly interpolate between the current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
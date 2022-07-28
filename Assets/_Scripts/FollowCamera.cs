using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private float smoothSpeed = 10.0f;
    private Vector3 currentVelocity = Vector3.zero;

    private void LateUpdate() {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref currentVelocity, smoothSpeed);
    }
}

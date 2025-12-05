using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0,2,-6);

    public float rotationSpeed = 1.0f;
    private void LateUpdate()
    {
        if (target == null) return;
        transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
        transform.position = target.position + (transform.position - target.position).normalized * offset.magnitude;
        transform.LookAt(target);
    }
}

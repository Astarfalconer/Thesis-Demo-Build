using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    public float pitch = 2f;

    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 100f;
    public float yawSpeed = 100f;
    private float yawInput = 0f;
    private float currentZoom = 10f;

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        
         yawInput -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }
    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, yawInput);
    }
}


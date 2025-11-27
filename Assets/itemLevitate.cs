using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemLevitate : MonoBehaviour
{
    Vector3 startPos;
    public float rotateSpeed = 50f;
    public float levitateAmplitude = 0.5f;
    public float levitateFrequency = 1f;
    // Start is called before the first frame update
    void Start()
    {
       
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime,Space.World);
        float newY = startPos.y + Mathf.Sin(Time.time * levitateFrequency) * levitateAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    public float rotationSpeed = 20;
    public float movementSpeed = 40;
    public float maxDistance = 100;
    public float zoomSpeed = 20;
    public float currentDistance = 5;
    public GameObject cameras;
    private Vector3 motion;
    private Vector3 rotation;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y-Input.GetAxis("Mouse Y")*movementSpeed*Time.deltaTime, 0, 1000), transform.position.z);
        }
        if (Input.GetButton("Fire2"))
        {
            rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0);
            transform.Rotate(rotation * (rotationSpeed * Time.deltaTime * 10));   
        }
        
        
        currentDistance += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, maxDistance,-2);
        cameras.transform.localPosition = Vector3.Lerp(cameras.transform.localPosition, new Vector3(0, 5, currentDistance), Time.deltaTime*3);
    }
}

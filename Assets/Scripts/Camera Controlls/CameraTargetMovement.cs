using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraTargetMovement : MonoBehaviour
{
    
    public float moveSpeed = 10f;
    public float rotateSpeed = 30f;
    private Vector3 motion;
    private Vector3 rotation;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            motion = new Vector3(0, -Input.GetAxis("Mouse Y"), 0);
            if (!(transform.position.y < 0.1f && motion.y < 0))
                rb.velocity = motion * moveSpeed;
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0, 1000),
            transform.position.z);
        
        if (Input.GetButton("Fire2"))
        {
            rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0);
            transform.Rotate(rotation * rotateSpeed/6);
        }
        
    }
}

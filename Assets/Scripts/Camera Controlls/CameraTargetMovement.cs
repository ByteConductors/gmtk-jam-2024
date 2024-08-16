using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetMovement : MonoBehaviour
{
    
    public float speed = 10f;
    private Vector3 motion;
    private Vector3 rotation;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            motion = new Vector3(0, Input.GetAxis("Mouse Y"), 0);
            rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0);
            rb.velocity = motion * speed;
            transform.Rotate(rotation * speed/6);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject cube;
    private bool _wasDown = false;
    public LayerMask sideLayerMask;
    public float breakForce;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _wasDown = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;

            if(Physics.Raycast(ray, out hitData, 1000, sideLayerMask))
            {
                if (hitData.transform.GetComponent<CubeGenerator>().placeable(new Vector3(Mathf.Round(hitData.normal.x),Mathf.Round(hitData.normal.y),Mathf.Round(hitData.normal.z))))
                {
                    var newCube = Instantiate(cube);
                    newCube.transform.position = hitData.transform.position + hitData.normal;
                    newCube.transform.rotation = hitData.transform.rotation;
                    var joint =  hitData.transform.GameObject().AddComponent<FixedJoint>();
                    joint.connectedBody = newCube.GetComponent<Rigidbody>();
                    joint.breakForce = breakForce;
                }
            }
        }
        else
        {
            _wasDown = false;
        }
    }
}

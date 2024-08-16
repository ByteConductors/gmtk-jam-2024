using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTracker : MonoBehaviour
{

    public Transform trackedObject;
    public float maxDistance = 10;
    public float moveSpeed = 20;
    public float updateSpeed = 10;
    [Range(0,10)]
    public float currentDistance = 5;
    private string moveAxis = "Mouse ScrollWheel";
    private GameObject ahead;
    private MeshRenderer meshRenderer;
    public float hideDistance = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        ahead = new GameObject("ahead");
        meshRenderer = trackedObject.gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ahead.transform.position = trackedObject.position + trackedObject.forward * (maxDistance * 0.25f);
        currentDistance += Input.GetAxis(moveAxis) * moveSpeed * Time.deltaTime;
        currentDistance = Mathf.Clamp(currentDistance, 0, maxDistance);
        transform.position = Vector3.MoveTowards(
            transform.position,
            trackedObject.position + Vector3.up * maxDistance - trackedObject.forward * (currentDistance + maxDistance * 0.25f), 
            updateSpeed * Time.deltaTime);
        transform.LookAt(ahead.transform);
        meshRenderer.enabled = (currentDistance > hideDistance);
    }
}

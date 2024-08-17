using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildSystem : MonoBehaviour
{
    public Camera playerCamera;
    [FormerlySerializedAs("cube")] public GameObject cubePrefab;
    public LayerMask sideLayerMask;
    public float breakForce;

    private Vector3 point = Vector3.zero;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            RaycastHit hitData;

            if(Physics.Raycast(ray, out hitData, 1000, sideLayerMask))
            {
                Debug.Log("Hit");
                Vector3 direction = new Vector3(Mathf.Round(hitData.normal.x), Mathf.Round(hitData.normal.y),
                    Mathf.Round(hitData.normal.z));
                var cube = hitData.transform.GetComponent<BuildingCube>();
                if (cube.placeable(direction))
                {
                    var newCube = Instantiate(cubePrefab, Tower.Instance.transform).GetComponent<BuildingCube>();
                    newCube.OnGridLocation = BuildingCube.CalculateLocation(cube.OnGridLocation, direction);
                    newCube.transform.position = hitData.transform.position + hitData.normal;
                    newCube.transform.rotation = hitData.transform.rotation;
                    newCube.GetComponent<AudioSource>().Play();

                    if (!Tower.Instance.IsSupported(newCube.OnGridLocation, out var unsupportedBlocks))
                    {
                        foreach (var position in unsupportedBlocks)
                        {
                            Debug.Log(position.ToString());
                            Tower.Instance.components[position].rb.isKinematic = false;
                        }
                    }
                    else
                    {
                        foreach (var position in unsupportedBlocks)
                            Debug.Log(position.ToString());
                        Debug.Log("Is Supported!");
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point,0.1f);
    }
}

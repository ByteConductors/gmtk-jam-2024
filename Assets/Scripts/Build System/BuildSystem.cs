using System;
using System.Collections;
using System.Collections.Generic;
using Build_System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;
using Workers;

public class BuildSystem : MonoBehaviour
{
    public Camera playerCamera;
    [FormerlySerializedAs("cube")] public GameObject cubePrefab;
    public LayerMask sideLayerMask;
    public float breakForce;
    public BlockQueue Queue;

    private Vector3 point = Vector3.zero;
    
    private Boolean isPaused = false;
    
    public delegate void BlockFall();
    public static event BlockFall BlockFalling;
    public float despawnDelay = 1.5f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPaused)
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
                var selectedQueueBlock = Queue.GetCurrentSelectedBlock();
                if (cube.placeable(direction,selectedQueueBlock.Shape))
                {
                    
                    var newCube = Instantiate(cubePrefab, Tower.Instance.transform).GetComponent<BuildingCube>();
                    newCube.OnGridLocation = BuildingCube.CalculateLocation(cube.OnGridLocation, direction);
                    newCube.transform.position = hitData.transform.position + hitData.normal;
                    newCube.transform.rotation = hitData.transform.rotation;
                    newCube.SetColor(selectedQueueBlock.WorkerColor);
                    
                    WorkerManager.Instance.AddColorSpace(selectedQueueBlock.WorkerColor);
                    
                    Queue.RemoveBlock(Queue.SelectedBlock);
                    newCube.GetComponent<AudioSource>().Play();

                    if (!Tower.Instance.IsSupported(newCube.OnGridLocation, out var unsupportedBlocks))
                    {
                        foreach (var position in unsupportedBlocks)
                        {
                            var _cube = Tower.Instance.components[position];
                            Debug.Log(position.ToString());
                            Tower.Instance.components[position].rb.isKinematic = false;
                            Tower.Instance.components.Remove(position);
                            if (BlockFalling != null)
                            {
                                BlockFalling();
                            }

                            StartCoroutine(DeleteObjekt(_cube.gameObject));
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
    
    IEnumerator DeleteObjekt(GameObject gameObject)
    {
        yield return new WaitForSeconds(despawnDelay);
        Destroy(gameObject);
        
    }

    private void Start()
    {
        GameManager.Instance.GamePause.AddListener((paused) => isPaused = paused);
    }
}

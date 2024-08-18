using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Build_System;
using Core;
using Unity.VisualScripting;
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
    
    public delegate void BlockFall();
    public static event BlockFall BlockFalling;
    public float despawnDelay = 1.5f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.GetIsPaused())
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
                        HandleUnstable(unsupportedBlocks);
                        CheckIntegrity();
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

    private void CheckIntegrity()
    {
        var keys = Tower.Instance.components.Keys.ToList();
        var stable = new List<Vector3Int>();
        for (int i = 0; i < keys.Count; i++)
        {
            if (!Tower.Instance.components.ContainsKey(keys[i])) continue;
            var potentiallyUnstableBlocks = new List<Vector3Int>();
            if (!Tower.Instance.CheckIfConnected(keys[i], potentiallyUnstableBlocks, stable))
                HandleUnstable(potentiallyUnstableBlocks);
            else
                stable.AddRange(potentiallyUnstableBlocks);
        }
    }

    private void HandleUnstable(List<Vector3Int> unstableBlocks)
    {
        foreach (var position in unstableBlocks)
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point,0.1f);
    }

    IEnumerator DeleteObjekt(GameObject gameObject)
    {
        yield return new WaitForSeconds(despawnDelay);
        Destroy(gameObject);
    }
}

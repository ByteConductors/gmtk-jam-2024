using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Color_Palettes;
using UnityEngine;
using UnityEngine.UIElements;
using Workers;

public class UI_WorkerQueue : MonoBehaviour
{
    
    public GameObject worker_prefab;
    [SerializeField] private ColorPalette _palette;
    private Queue<GameObject> workers = new Queue<GameObject>();
    private GameObject newWorker;
    
    void newDudeInLine(WorkerColor workerColor)
    {
        Color color = _palette.colors[(int)workerColor + 1];
        newWorker = Instantiate(worker_prefab, transform);
        newWorker.transform.position = new Vector3(20, 20 + (workers.Count * 20), 0);
        newWorker.GetComponent<UI_QueueIcon>().setIconColor(color);
        newWorker.transform.SetSiblingIndex(workers.Count);
        workers.Enqueue(newWorker);
    }

    void deleteDudeInLine()
    {
        Debug.Log("Worker killed, remaining: " + workers.Count);
        GameObject pleasedWorker = workers.Dequeue();
        Destroy(pleasedWorker);
        foreach (var dude in workers)
        {
            dude.transform.position -= new Vector3(0, 20, 0);
        }
        
    }

    void Start()
    {
        WorkerManager.Instance.onNewWorkerSpaceRequested.AddListener(newDudeInLine);
    }
}

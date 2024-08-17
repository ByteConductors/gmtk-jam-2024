using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Color_Palettes;
using UnityEngine;
using UnityEngine.UIElements;
using Workers;

public class UI_WorkerQueue : MonoBehaviour
{
    
    public GameObject worker_prefab;
    [SerializeField] private ColorPalette _palette;
    public int maxWorkers = 10;
    private List<GameObject> workers = new List<GameObject>();
    private GameObject newWorker;
    
    void newDudeInLine(WorkerColor workerColor)
    {
        Color color = _palette.colors[(int)workerColor + 1];
        Debug.Log("New dude in line");
        Debug.Log(color);
        newWorker = Instantiate(worker_prefab, new Vector3(0, 0, 0), Quaternion.identity);
        newWorker.transform.SetParent(transform);
        newWorker.transform.position = new Vector3(20, 20 + (workers.Count * 40), 0);

        newWorker.GetComponent<UI_QueueIcon>().setIconColor(color);
        
        workers.Add(newWorker);
        Debug.Log(workers.Count.ToString());
        
    }

    void Start()
    {
        WorkerManager.Instance.onNewWorkerSpaceRequested.AddListener(newDudeInLine);
    }
}

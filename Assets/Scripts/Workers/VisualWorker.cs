using System;
using System.Collections;
using System.Collections.Generic;
using Color_Palettes;
using UnityEngine;
using Workers;

public class VisualWorker : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> waitingPositions;
    public GameObject workerPrefab;
    private List<GameObject> _waitingDic;
    public GameObject spawnPoint;
    [SerializeField] private ColorPalette palette;
    void Start()
    {
        _waitingDic = new List<GameObject>();
        WorkerManager.Instance.onNewWorkerSpaceRequested.AddListener(SpawnNewWorker);
        WorkerManager.Instance.onWorkerQueueRelieved.AddListener(_=>RelivesWorker());
    }

    void SpawnNewWorker(WorkerColor workerColor)
    {
        var newWorker = Instantiate(workerPrefab);
        _waitingDic.Add(newWorker);
        newWorker.GetComponent<MeshRenderer>().material.color = palette.colors[(int)workerColor + 1];
        newWorker.transform.position = spawnPoint.transform.position;
        StartCoroutine(MoveToSpot(newWorker, spawnPoint.transform.position,
            waitingPositions[_waitingDic.Count - 1].transform.position));
    }

    void RelivesWorker()
    {
        for (int i = 0; i < _waitingDic.Count; i++)
        {
            if (i == 0)
            {
                StartCoroutine(MoveToSpot(_waitingDic[i], _waitingDic[i].transform.position, new Vector3(0, 0.5f, 0), true));
            }
            else
            {
                StartCoroutine(MoveToSpot(_waitingDic[i], _waitingDic[i].transform.position, waitingPositions[i-1].transform.position));
            }
        }
        _waitingDic.RemoveAt(0);
    }
    IEnumerator MoveToSpot(GameObject gameObject, Vector3 currentPos, Vector3 targetPos, bool delete = false)
    {
        gameObject.transform.position = currentPos;
        float elapsedTime = 0;
        float waitTime = (targetPos-currentPos).magnitude/5;
        while (elapsedTime < waitTime)
        {
            gameObject.transform.position = Vector3.Lerp(currentPos, targetPos, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = targetPos;
        if (delete)
        {
            Destroy(gameObject);
        }
        yield return null;
    }
}

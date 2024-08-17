using System.Collections.Generic;
using Color_Palettes;
using Core;
using UnityEngine;
using Workers;

namespace UI.ingame
{
    public class UI_WorkerQueue : MonoBehaviour
    {
    
        public GameObject workerPrefab;
        [SerializeField] private ColorPalette palette;
        private Queue<GameObject> _workers = new Queue<GameObject>();
        private GameObject _newWorker;
        [SerializeField] private float workerDistance = 20f;
    
        void NewDudeInLine(WorkerColor workerColor)
        {
            Color color = palette.displayColors[(int)workerColor + 1];
            _newWorker = Instantiate(workerPrefab, transform);
            _newWorker.transform.position = new Vector3(20, 20 + (_workers.Count * workerDistance), 0);
            _newWorker.GetComponent<UIQueueIcon>().SetIconColor(color);
            _newWorker.transform.SetSiblingIndex(_workers.Count);
            _workers.Enqueue(_newWorker);
        }

        void DeleteDudeInLine()
        {  
            GameObject pleasedWorker = _workers.Dequeue();
            Destroy(pleasedWorker);
            foreach (var dude in _workers)
            {
                dude.transform.position -= new Vector3(0, workerDistance, 0);
            }
        }

        void OnGameOver()
        {
            Debug.Log("Game Over - from WorkerQueue");
            WorkerManager.Instance.onNewWorkerSpaceRequested.RemoveListener(NewDudeInLine);
            WorkerManager.Instance.onNewWorkerSpaceRequested.RemoveListener(_ => DeleteDudeInLine());
        }
        
        void Start()
        {
            WorkerManager.Instance.onNewWorkerSpaceRequested.AddListener(NewDudeInLine);
            WorkerManager.Instance.onWorkerQueueRelieved.AddListener(_ => DeleteDudeInLine());
            GameManager.Instance.GameOver.AddListener(OnGameOver);
        }
    }
}

using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Workers
{
    public class WorkerManager : MonoBehaviour
    {
        public UnityEvent<WorkerColor> onNewWorkerSpaceRequested = new();
        public float spaceRequestDelayInSeconds = 5;

        private readonly Queue<WorkerColor> _colorsQueue = new();
        public Queue<WorkerColor> WorkerQueue => _colorsQueue;

        private readonly Dictionary<WorkerColor, int> _freeSpaceTable = new()
        {
            { WorkerColor.Red, 0 },
            { WorkerColor.Green, 0 },
            { WorkerColor.Blue, 0 },
            { WorkerColor.Purple, 0 }
        };

        private float _nextSpaceRequestTime;

        public static WorkerManager Instance => _instance;
        private static WorkerManager _instance;

        private void Awake()
        {
            DoSingletonCheck();
        }

        private void FixedUpdate()
        {
            DoTimerCheck();
        }

        private void DoTimerCheck()
        {
            if (_nextSpaceRequestTime >= Time.time) return;
            _nextSpaceRequestTime += spaceRequestDelayInSeconds;
            SendNewSpaceRequest();
        }

        private void SendNewSpaceRequest()
        {
            var color = (WorkerColor)Random.Range(0, 4);
            _colorsQueue.Enqueue(color);
            onNewWorkerSpaceRequested.Invoke(color);
            RelieveQueue();
        }

        private void RelieveQueue()
        {
            for (var i = 0; i < _colorsQueue.Count; i++)
            {
                if (!_colorsQueue.TryPeek(out var color) || _freeSpaceTable[color] <= 0) return;

                _freeSpaceTable[color]--;
                _colorsQueue.Dequeue();
            }
        }

        private void DoSingletonCheck()
        {
            if (!Instance)
            {
                _instance = this;
                return;
            }
            
            Debug.Log("Instance already exists, deleting!");
            Destroy(gameObject);
        }

        public void AddColorSpace(WorkerColor color)
        {
            _freeSpaceTable[color]++;
        }
    }
}
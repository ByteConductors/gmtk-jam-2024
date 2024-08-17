using System;
using System.Collections.Generic;
using System.Text;
using Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Workers
{
    public class WorkerManager : MonoBehaviour
    {
        public UnityEvent<WorkerColor> onNewWorkerSpaceRequested = new();
        public UnityEvent<WorkerColor> onWorkerQueueRelieved = new();
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
        
        private Boolean isPaused = false;
    
        private void Awake()
        {
            DoSingletonCheck();
            _nextSpaceRequestTime = Time.time;
        }

        private void Start()
        {
            GameManager.Instance.GameOver.AddListener(GameOver);
            GameManager.Instance.GamePause.AddListener((paused) => isPaused = paused);
        }

        private void FixedUpdate()
        {
            DoTimerCheck();
        }

        private void GameOver()
        {
            _colorsQueue.Clear();
        }

        private void DoTimerCheck()
        {
            if (_nextSpaceRequestTime >= Time.time) return;
            _nextSpaceRequestTime = spaceRequestDelayInSeconds + Time.time;
            if (!isPaused) SendNewSpaceRequest();
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
                onWorkerQueueRelieved.Invoke(_colorsQueue.Dequeue());
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
            RelieveQueue();
        }
    }
}
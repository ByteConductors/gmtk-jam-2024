using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using Workers;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;
        
        public readonly UnityEvent GameOver = new ();
        public readonly UnityEvent GameBegin = new ();

        [SerializeField] private int maxQueueLength;

        private void Awake()
        {
            if (_instance)
            {
                Debug.LogWarning("Instance already exists, destroying this one");
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        private void Start()
        {
            WorkerManager.Instance.onNewWorkerSpaceRequested.AddListener(_ => CheckGameOver());
        }

        private void CheckGameOver()
        {
            if (WorkerManager.Instance?.WorkerQueue.Count < maxQueueLength) return;
            TriggerGameOver();
        }

        public void TriggerGameOver()
        {
            GameOver.Invoke();
        }
    }
}


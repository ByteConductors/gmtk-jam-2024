using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using Workers;
using Object = UnityEngine.Object;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;
        
        public readonly UnityEvent<String> GameOver = new ();
        public readonly UnityEvent GameBegin = new ();
        public readonly UnityEvent<Boolean> GamePause = new ();

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
            TriggerGameOver("Your queue is too long");
        }

        public void TriggerGameOver(string Reason)
        {
            GameOver.Invoke(Reason);
            _isPaused = true;
            GamePause.Invoke(_isPaused);
        }

        private bool _isPaused = false;
        public void TriggerPause()
        {
            Debug.Log("Pause initiated");
            _isPaused = !_isPaused;
            GamePause.Invoke(_isPaused);
        }
        
        public Boolean getIsPaused() => _isPaused;
    }
}


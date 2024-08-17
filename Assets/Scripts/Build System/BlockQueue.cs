using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Events;
using Workers;
using Random = UnityEngine.Random;

namespace Build_System
{
    public class BlockQueue : MonoBehaviour
    {
        public class GeneratedBlock
        {
            private readonly BuildShape _shape = (BuildShape)Random.Range(0, 4);
            public BuildShape Shape => _shape;
            private readonly WorkerColor _workerColor = (WorkerColor)Random.Range(0, 4);
            public WorkerColor WorkerColor => _workerColor;

            public GeneratedBlock() { }
            public GeneratedBlock(WorkerColor workerColor)
            {
                _workerColor = workerColor;
            }
        }

        private List<GeneratedBlock> _blocks = new ();
        public List<GeneratedBlock> Blocks => _blocks;

        public UnityEvent<GeneratedBlock> OnBlockGenerate = new();
        public UnityEvent<GeneratedBlock> OnBlockDestroy = new();
        public GeneratedBlock SelectedBlock { get; set; }
        private int blockType = 0;
        
        [SerializeField] private float nextBlockDelay = 5;
        private float nextBlockTime = 0;
        
        private Boolean isPaused = false;

        public void GenerateBlocks(int count)
        {
            for (int i = 0; i < count; i++) GenerateBlock();
        }

        private void FixedUpdate()
        {
            if (Time.time >= nextBlockTime && !isPaused)
            {
                nextBlockTime = Time.time + nextBlockDelay;
                GenerateBlock();
            }
        }

        public GeneratedBlock GenerateBlock()
        {
            if (_blocks.Count >= 5) return null;
            if (blockType == 4) blockType = 0;
            var block = new GeneratedBlock((WorkerColor)blockType++);
            _blocks.Add(block);
            OnBlockGenerate.Invoke(block);
            return block;
        }

        public void RemoveBlock(GeneratedBlock block)
        {
            _blocks.Remove(block);
            OnBlockDestroy.Invoke(block);
        }

        private void Start()
        {
            GenerateBlocks(5);
            GameManager.Instance.GamePause.AddListener((paused) => isPaused = paused);
        }

        public bool HasBlock()
        {
            return _blocks.Count > 0;
        }

        public GeneratedBlock GetCurrentSelectedBlock()
        {
            return SelectedBlock;
        }
    }
}
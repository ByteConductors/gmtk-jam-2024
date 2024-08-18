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
        
        public float minSpaceRequestDelayInSeconds = 0.1f;
        public float maxSpaceRequestDelayInSeconds = 3;
        public float streatchFunction = 40;
        private long iteration = 1;

        public void GenerateBlocks(int count)
        {
            for (int i = 0; i < count; i++) GenerateBlock();
        }

        private void FixedUpdate()
        {
            if (Time.time >= nextBlockTime && !GameManager.Instance.GetIsPaused())
            {
                nextBlockTime = (Mathf.Exp(-(iteration/streatchFunction))*maxSpaceRequestDelayInSeconds + minSpaceRequestDelayInSeconds)/2.0f+ Time.time;
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
            SelectedBlock = null;
        }

        private void Start()
        {
            GenerateBlocks(5);
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
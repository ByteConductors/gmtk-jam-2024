using System;
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
        }

        private GeneratedBlock[] _blocks = new GeneratedBlock[5];
        public GeneratedBlock[] Blocks => _blocks;

        public UnityEvent<int, GeneratedBlock> OnBlockGenerate = new();
        public int SelectedBlock { get; set; }

        public GeneratedBlock[] GenerateBlocks()
        {
            for (int i = 0; i < _blocks.Length; i++) RerollSlot(i);
            return _blocks;
        }

        public void RerollSlot(int i)
        {
            _blocks[i] = new GeneratedBlock();
            OnBlockGenerate.Invoke(i,_blocks[i]);
        }

        private void Start()
        {
            GenerateBlocks();
        }
    }
}
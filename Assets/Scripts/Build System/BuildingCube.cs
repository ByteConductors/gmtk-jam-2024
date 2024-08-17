using System;
using UnityEngine;
using Workers;
using Random = UnityEngine.Random;

namespace Build_System
{
    public class BuildingCube : MonoBehaviour
    {
        [SerializeField] public Color[] colors;
        [SerializeField] public Side[] sideObjects;
        [SerializeField] private int colorIndex = -1;

        [SerializeField] public Boolean isInitialBlock = false;
    
        private Color _color;
        private Vector3Int _onGridLocation;
        public Vector3Int OnGridLocation
        {
            get => _onGridLocation; 
            set
            {
                _onGridLocation = value;
                Tower.Instance.PlaceBlocks(value, this);
            }
        }

        public Rigidbody rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            sideObjects = GetComponentsInChildren<Side>();
            if (colorIndex >= 0) SetColor((WorkerColor)colorIndex);
        }

        public void SetColor(WorkerColor color)
        {
            var index = (int)color;
            GetComponent<MeshRenderer>().material.color = colors[index];
            _color = colors[index];
        }

        public bool placeable(Vector3 normal, BuildShape shape)
        {
            Vector3Int direction = new Vector3Int(Mathf.RoundToInt(normal.x), Mathf.RoundToInt(normal.y),
                Mathf.RoundToInt(normal.z));
            if (GetSideInDirection(direction).shape != shape) return false;
        
            Vector3Int newPosition = CalculateLocation(OnGridLocation, normal);
            return Tower.Instance.IsLocationFree(newPosition);
        }

        public Side GetSideInDirection(Vector3Int direction)
        {
            foreach (var side in sideObjects)
            {
                if (side.direction == direction) return side;
            }

            return null;
        }
    
        public static Vector3Int CalculateLocation(Vector3Int location, Vector3 direction)
        {
            return location + new Vector3Int(Mathf.RoundToInt(direction.x), Mathf.RoundToInt(direction.y),
                Mathf.RoundToInt(direction.z));
        }
    }
}

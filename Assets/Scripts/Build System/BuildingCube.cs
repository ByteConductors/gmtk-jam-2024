using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuildingCube : MonoBehaviour
{
    public Color[] colors;

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
        var index = Random.Range(0, colors.Length);
        GetComponent<MeshRenderer>().material.color = colors[index];
        _color = colors[index];
    }

    public bool placeable(Vector3 normal)
    {
        Vector3Int newPosition = CalculateLocation(OnGridLocation, normal);
        return Tower.Instance.IsLocationFree(newPosition);
    }
    
    public static Vector3Int CalculateLocation(Vector3Int location, Vector3 direction)
    {
        return location + new Vector3Int(Mathf.RoundToInt(direction.x), Mathf.RoundToInt(direction.y),
            Mathf.RoundToInt(direction.z));
    }
    
    private class Side
    {
        private Color _color;
        private GameObject _gameObject;
        private Vector3 _normal;
        private bool _blocked;

        public Side(Color color, GameObject gameObject, Vector3 normal)
        {
            _color = color;
            _gameObject = gameObject;
            _normal = normal;
        }

        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        public GameObject GameObject
        {
            get => _gameObject;
            set => _gameObject = value;
        }

        public Vector3 Normal
        {
            get => _normal;
            set => _normal = value;
        }

        public bool Blocked
        {
            get => _blocked;
            set => _blocked = value;
        }
    }
}

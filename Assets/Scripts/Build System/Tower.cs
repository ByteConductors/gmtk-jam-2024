using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Build_System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private static Tower _instance;
    public static Tower Instance => _instance;
    
    [SerializeField] private Texture2D[] shapeTextures;
    public Texture2D[] ShapeTextures => shapeTextures;

    public BuildingCube initialCube;

    [SerializeField] private int MAX_SEARCH_DEPTH = 5;

    public readonly Dictionary<Vector3Int, BuildingCube> components = new();

    private void Awake()
    {
        _instance = this;
        PlaceBlocks(Vector3Int.zero, initialCube);
    }

    public bool PlaceBlocks(Vector3Int location, BuildingCube cube)
    {
        return components.TryAdd(location, cube);
    }

    public bool IsLocationFree(Vector3Int location)
    {
        return !components.ContainsKey(location);
    }

    public bool IsSupported(Vector3Int location, out List<Vector3Int> list)
    {
        list = new List<Vector3Int>();
        if (location.y == 0) return true;
        return _isSupported(location, 0, list);
    }

    private bool _isSupported(Vector3Int location, int depth, List<Vector3Int> covered)
    {
        if (depth == MAX_SEARCH_DEPTH) return false;
        if (IsLocationFree(location)) return false;
        if (!IsLocationFree(location + Vector3Int.down)) return true;
        
        covered.Add(location);
        var nextLocation = location + Vector3Int.back;
        if (!covered.Contains(nextLocation) && _isSupported(nextLocation, depth + 1, covered)) return true;
        
        nextLocation = location + Vector3Int.forward;
        if (!covered.Contains(nextLocation) && _isSupported(nextLocation, depth + 1, covered)) return true;
        
        nextLocation = location + Vector3Int.left;
        if (!covered.Contains(nextLocation) && _isSupported(nextLocation, depth + 1, covered)) return true;
        
        nextLocation = location + Vector3Int.right;
        if (!covered.Contains(nextLocation) && _isSupported(nextLocation, depth + 1, covered)) return true;

        return false;
    }
}

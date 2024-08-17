using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Side : MonoBehaviour
{
    public Vector3Int direction;
    public BuildShape shape;
    private MeshRenderer _renderer;
    
    
    private void Start()
    {
        shape = (BuildShape)Random.Range(0, 4);
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.SetTexture("_MainTex",Tower.Instance.ShapeTextures[(int)shape]);
    }
}

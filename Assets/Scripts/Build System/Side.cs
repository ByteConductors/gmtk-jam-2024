using System;
using Build_System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Side : MonoBehaviour
{
    public Vector3Int direction;
    public BuildShape shape;
    private MeshRenderer _renderer;
    
    private enum InitialBlockSides
    {
        Right, //0
        Left, //1
        Front, //2
        Back, //3
        Top, //4
        Bottom, //5
    }
    
    private void Start()
    {
        Enum.TryParse(name, out InitialBlockSides side);
        if (transform.parent.GetComponent<BuildingCube>().isInitialBlock && (int)side < 4)
        {
            shape = (BuildShape)(int)side;
        }
        else
        {
            shape = (BuildShape)Random.Range(0, 4);
        }

        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.SetTexture("_MainTex",Tower.Instance.ShapeTextures[(int)shape]);
    }
}

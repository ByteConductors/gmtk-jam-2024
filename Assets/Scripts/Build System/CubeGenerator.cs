using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeGenerator : MonoBehaviour
{
    public Color[] colors;
    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;
    public GameObject front;
    public GameObject back;

    private Color _color;
    private Color _topColor;
    private Color _bottomColor;
    private Color _leftColor;
    private Color _rightColor;
    private Color _frontColor;
    private Color _backColor;
    
    public bool topBlocked;
    public bool bottomBlocked;
    public bool leftBlocked;
    public bool rightBlocked;
    public bool frontBlocked;
    public bool backBlocked;
    
    

    void Start()
    {
        var index = Random.Range(0, colors.Length);
        GetComponent<MeshRenderer>().material.color = colors[index];
        _color = colors[index];
        
        index = Random.Range(0, colors.Length);
        top.GetComponent<MeshRenderer>().material.color = colors[index];
        _topColor = colors[index];
        
        index = Random.Range(0, colors.Length);
        bottom.GetComponent<MeshRenderer>().material.color = colors[index];
        _bottomColor = colors[index];
        
        index = Random.Range(0, colors.Length);
        left.GetComponent<MeshRenderer>().material.color = colors[index];
        _leftColor = colors[index];
        
        index = Random.Range(0, colors.Length);
        right.GetComponent<MeshRenderer>().material.color = colors[index];
        _rightColor = colors[index];
        
        index = Random.Range(0, colors.Length);
        front.GetComponent<MeshRenderer>().material.color = colors[index];
        _frontColor = colors[index];
        
        index = Random.Range(0, colors.Length);
        back.GetComponent<MeshRenderer>().material.color = colors[index];
        _backColor = colors[index];
    }

    public bool placeable(Vector3 normal)
    {
        Debug.Log(normal);
        if (normal.Equals(Vector3.up) && !topBlocked)
        {
            topBlocked = true;
        } else if (normal.Equals(Vector3.down) && !bottomBlocked)
        {
            bottomBlocked = true;
        }else if (normal.Equals(Vector3.forward) && !frontBlocked)
        {
            frontBlocked = true;
        }else if (normal.Equals(Vector3.back) && !backBlocked)
        {
            backBlocked = true;
        }else if (normal.Equals(Vector3.right) && !rightBlocked)
        {
            rightBlocked = true;
        }else if (normal.Equals(Vector3.down) && !leftBlocked)
        {
            leftBlocked = true;
        }
        else
        {
            return false;
        }

        return true;
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

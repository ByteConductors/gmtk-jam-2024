using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

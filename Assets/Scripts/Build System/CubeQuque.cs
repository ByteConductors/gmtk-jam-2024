using System.Collections.Generic;
using UnityEngine;

public class CubeQuque : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] queuePlaceholder;
    public GameObject cube;
    private Dictionary<int,(GameObject, Vector3)> _cubes;
    void Start()
    {
        _cubes = new Dictionary<int, (GameObject, Vector3)>();
        for (var i = 0; i < queuePlaceholder.Length; i++)
        {
            var newCube = Instantiate(cube);
            newCube.transform.position = queuePlaceholder[i].transform.position;
            _cubes.Add(i, (newCube, queuePlaceholder[i].transform.position));
        }
    }

    private void Update()
    {
        for (var i = 0; i < queuePlaceholder.Length; i++)
        {
            if (_cubes[i].Item1.layer == 7) continue;
            var newCube = Instantiate(cube);
            newCube.transform.position = _cubes[i].Item2;
            var pos = _cubes[i].Item2;
            _cubes.Remove(i);
            _cubes.Add(i, (newCube, pos));
            return;
        }
    }
}

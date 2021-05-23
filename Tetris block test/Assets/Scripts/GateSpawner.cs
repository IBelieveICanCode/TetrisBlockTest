using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    [SerializeField]
    Renderer _bottom;

    private void Start()
    {        
        
        int cubesInRow = Mathf.RoundToInt((_bottom.bounds.size.z /  0.11f));
        Debug.Log(_bottom.bounds.size.x / 8);
        float threshold = 0;
        for (int i = 0; i < cubesInRow; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(_bottom.bounds.max.x - 0.05f, 0.1f, _bottom.bounds.max.z - 0.05f - threshold);
            threshold += (_bottom.bounds.size.x/ 9) + 0.1f;//25f;
            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            cube.transform.parent = this.transform;
        }
        
        
    }
}

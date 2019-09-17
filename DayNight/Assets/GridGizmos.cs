using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGizmos : MonoBehaviour
{
    public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        Gizmos.DrawSphere(tilemap.origin, .1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

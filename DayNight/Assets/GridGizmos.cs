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
        
    }

    void OnDrawGizmos() {
        if (tilemap != null) {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(tilemap.origin, .25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

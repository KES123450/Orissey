using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainData : MonoBehaviour
{
    public void Init(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        cp1 = p1;
        cp2 = p2;
        cp3 = p3;
        cp4 = p4;
    }

    public Vector3 cp1;
    public Vector3 cp2;
    public Vector3 cp3;
    public Vector3 cp4;
    
}

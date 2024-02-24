using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonToEdge : MonoBehaviour
{
    private void Start()
    {
        EdgeCollider2D edge= gameObject.AddComponent<EdgeCollider2D>();
        PolygonCollider2D polygon = GetComponent<PolygonCollider2D>();
        edge.points = polygon.points;
        Destroy(polygon);
    }
}

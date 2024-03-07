using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDistance : MonoBehaviour
{
    [SerializeField] private float rayDistance;
    private float playerToTerrainDistance;
    public float PlayerToTerrainDistance
    {
        get => playerToTerrainDistance;
        set => playerToTerrainDistance = value;
        
    }

    private void CalculateDistance()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, 1 << 6);
        PlayerToTerrainDistance = ray.fraction * rayDistance;
    }

    private void Update()
    {
        CalculateDistance();
    }
}

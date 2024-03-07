using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeParallaxEffect : MonoBehaviour
{
    private TerrainDistance terrainDistance;
    private float startPosY;
    [SerializeField] private float spaceY;

    private void Start()
    {
        terrainDistance = GameManager.Instance.Player.gameObject.GetComponent<TerrainDistance>();
        startPosY = transform.localPosition.y;
    }

    private void Update()
    {
        float distance = terrainDistance.PlayerToTerrainDistance / 10f;
        distance = Mathf.Lerp(startPosY, startPosY - spaceY, distance);
        transform.localPosition = new Vector3(transform.localPosition.x, distance, transform.localPosition.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveParallaxEffect : MonoBehaviour
{
    public Vector3 terrainStartPoint;
    public AnimationCurve terrainCurve;

    private float startposX, startposY;
    private float offsetX, offsetY;
    public float parallaxFactorX;
    public float parallaxFactorY;
    public GameObject cam;


    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        offsetX = startposX - terrainStartPoint.x;
        offsetY = startposY - (terrainCurve.Evaluate(offsetX) + terrainStartPoint.y);
    }


    void Update()
    {
        float distanceX = cam.transform.position.x * parallaxFactorX;
        float distanceY = terrainCurve.Evaluate(offsetX+distanceX);
        Vector3 newPosition = new Vector3(startposX + distanceX, terrainStartPoint.y + distanceY+ offsetY, transform.position.z);
        transform.position = newPosition;
    }
}

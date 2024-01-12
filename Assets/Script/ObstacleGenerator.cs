using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject testTerrain;

    public void CreateObstacle(GameObject terrain)
    {
        Vector2[] points = terrain.GetComponent<PolygonCollider2D>().points;

        int randomPoint = Random.Range(2, points.Length - 2);
        Vector3 obstaclePos = new Vector3(points[randomPoint].x, points[randomPoint].y, 2);
        obstaclePos += terrain.transform.position;

        float obstacleSlope = (points[randomPoint + 1].y - points[randomPoint - 1].y) /
            (points[randomPoint + 1].x - points[randomPoint - 1].x);
        float obstacleAngle = Mathf.Atan2(obstacleSlope, 1f) * Mathf.Rad2Deg;
        Quaternion obstacleRotation = Quaternion.Euler(new Vector3(0, 0, obstacleAngle));
        Instantiate(obstacle,obstaclePos , obstacleRotation);
    }
   
}

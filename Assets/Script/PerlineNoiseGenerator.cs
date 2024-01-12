using UnityEngine;

public class PerlineNoiseGenerator : MonoBehaviour
{
    public int numPoints = 100;
    public Vector3[] points;
    public float scale = 1.0f;
    public float xScale;
    public float yScale;
    public GameObject pointOBJ;

    void Start()
    {
        points = new Vector3[numPoints];
    }

    public void CreateTerrainpoints()
    {
        for (int i = 0; i < numPoints; i++)
        {
            float x = (float)i / (numPoints - 1);
            float y = Mathf.PerlinNoise(x * scale , 0.0f);
            points[i] = new Vector3(x * xScale, y * yScale, 0.0f);
            Instantiate(pointOBJ, points[i], Quaternion.identity);
        }
    }
}

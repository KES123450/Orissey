using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ObstacleGenerator))]
public class ObstacleGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ObstacleGenerator obstacleGenerator = (ObstacleGenerator)target;

        if (GUILayout.Button("ObstacleGenerate"))
        {
            obstacleGenerator.CreateObstacle(obstacleGenerator.testTerrain);
        }
    }
}


    


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontObjectSpawner : MonoBehaviour
{
    private Queue<GameObject> frontObjectQueue = new();
    private Dictionary<FrontObjectType, GameObject> objectDatas = new();
    private float playerMovementByX;
    private float prevPlayerPosByX;
    private int prevObjectIdx;
    private Transform frontLayer;
    [SerializeField] private float objectSpaceX;
    [SerializeField] private float objectOffsetY;
    [SerializeField] private float playerMovementOffset;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        frontLayer = GameObject.Find("FrontLayer").transform;
        objectDatas[FrontObjectType.Grass1] = Resources.Load("Prefab/FrontObject/Grass1") as GameObject;
        objectDatas[FrontObjectType.Grass2] = Resources.Load("Prefab/FrontObject/Grass2") as GameObject;
        objectDatas[FrontObjectType.Grass3] = Resources.Load("Prefab/FrontObject/Grass3") as GameObject;
        objectDatas[FrontObjectType.LongGrass1] = Resources.Load("Prefab/FrontObject/LongGrass1") as GameObject;
        objectDatas[FrontObjectType.LongGrass2] = Resources.Load("Prefab/FrontObject/LongGrass2") as GameObject;
        objectDatas[FrontObjectType.Ranunculus] = Resources.Load("Prefab/FrontObject/Ranunculus") as GameObject;
    }
    
   

    private void SpawnFrontObject()
    {
        GameObject terrain= GameManager.Instance.Player.PlayerOnTerrain;
        Vector2[] terrainPoints = terrain.GetComponent<PolygonCollider2D>().points;
        FrontObjectType selectedObjectType = (FrontObjectType)Random.Range(0, System.Enum.GetValues(typeof(FrontObjectType)).Length);
        GameObject frontObject = Instantiate(objectDatas[selectedObjectType], FindObjectSpawnPos(terrainPoints,terrain.transform), Quaternion.identity, frontLayer);
        frontObjectQueue.Enqueue(frontObject);
    }

    private void CalculatePlayerMovementForSpawn()
    {
        playerMovementByX += GameManager.Instance.Player.transform.position.x - prevPlayerPosByX;
        prevPlayerPosByX = GameManager.Instance.Player.transform.position.x;

        if(playerMovementByX >= playerMovementOffset)
        {
            SpawnFrontObject();
            //Destroy(frontObjectQueue.Dequeue());
            playerMovementByX = 0;

        }
    }

    private Vector3 FindObjectSpawnPos(Vector2[] terrainPoints,Transform terrain)
    {
        float nextObjectPosX = terrainPoints[prevObjectIdx].x + objectSpaceX;
        for(int i= prevObjectIdx; i < terrainPoints.Length; i++)
        {
            if(nextObjectPosX - terrainPoints[i].x < 0)
            {
                prevObjectIdx = i;
                return terrain.TransformPoint(new Vector3(terrainPoints[i].x, terrainPoints[i].y + objectOffsetY, 0));
            }
        }
        prevObjectIdx = 0;
       
        return terrain.TransformPoint(new Vector3(terrainPoints[terrainPoints.Length - 1].x,
            terrainPoints[terrainPoints.Length - 2].y + objectOffsetY,
            0));
    }

    private void FixedUpdate()
    {
        CalculatePlayerMovementForSpawn();
    }
}

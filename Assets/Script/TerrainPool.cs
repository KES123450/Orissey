using UnityEngine.Pool;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPool : MonoBehaviour
{
    public Dictionary<TerrainType, ObjectPool<Terrain>> terrainPoolDictionary;
    public List<Terrain> terrainPrefabs = new List<Terrain>();

    void Start()
    {
        terrainPoolDictionary = new Dictionary<TerrainType, ObjectPool<Terrain>>();
        int i = 0;
        foreach (Terrain terrain in terrainPrefabs)
        {
            TerrainType terrainType = (TerrainType)i;
            ObjectPool<Terrain> objectPool = new ObjectPool<Terrain>(
                () => CreateTerrain(terrain, terrainType),
                OnGet,
                OnRelease,
                OnDestroy,
                maxSize: 10
                );

            terrainPoolDictionary.Add(terrainType, objectPool);
            i++;

        }
    }

    private Terrain CreateTerrain(Terrain terrain, TerrainType type)
    {
        Terrain t = Instantiate(terrain);
        t.SetPool(terrainPoolDictionary[type]);
        return t;
    }

    private void OnGet(Terrain terrain)
    {
        terrain.gameObject.SetActive(true);
    }

    private void OnRelease(Terrain terrain)
    {
        terrain.time = 0;
        terrain.gameObject.SetActive(false);
    }

    private void OnDestroy(Terrain terrain)
    {
        Destroy(terrain);
    }

    public Terrain GetTerrain(TerrainType key)
    {
        return terrainPoolDictionary[key].Get();
    }

}

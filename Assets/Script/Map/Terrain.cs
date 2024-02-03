using UnityEngine;
using UnityEngine.Pool;

public class Terrain : MonoBehaviour
{
    private IObjectPool<Terrain> terrainPool;
    public float time;

    public void SetPool(IObjectPool<Terrain> pool)
    {
        terrainPool = pool;
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time >= 15f)
        {
            terrainPool.Release(this);
            time = 0;
        }
    }
}

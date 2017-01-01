using UnityEngine;
using System.Collections;

public class CreateWorld : MonoBehaviour
{
    public Vector3 spawnPoint;
    public GameObject actor;
    public int maxActors = 30;

    Vector3 GetRandomSpawnPoint()
    {
        Vector3 spawnRadius = Random.insideUnitSphere * 500f;
        spawnRadius.y = 0.0f;

        spawnPoint = spawnRadius + transform.position;

        return spawnPoint;
    }

    void SpawnObject()
    {
        int spawned = 0;

        while(spawned < maxActors)
        {
            Instantiate(actor, GetRandomSpawnPoint(), Quaternion.identity);
        }  
    }

    void Start()
    {
        SpawnObject();
    }
	
}

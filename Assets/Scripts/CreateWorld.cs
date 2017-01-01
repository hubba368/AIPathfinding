using UnityEngine;
using System.Collections;

public class CreateWorld : MonoBehaviour
{
    public Vector3 spawnPoint;
    public Quaternion rotation;

    public GameObject[] actors;
    public int maxActors = 30;  //max amount that can be spawned


    Vector3 GetRandomSpawnPoint()
    {
        //Generate a random spawn point by using insideUnitSphere
        Vector3 spawnRadius = Random.insideUnitSphere * 75f;
        spawnRadius.y = 0.0f;

        spawnPoint = spawnRadius + transform.position;

        return spawnPoint;
    }

    void SpawnObject()
    {
        //Spawn objects that are set in the inspector
        int spawned = 0;
        

        float direction = Random.Range(0f, 360f);

        while(spawned < maxActors)
        {
            //spawn until limit reached
            int prefab = Random.Range(0, 3);
            Instantiate(actors[prefab], GetRandomSpawnPoint(), Quaternion.Euler(new Vector3(0f, direction, 0f)));
            spawned++;
        }  
    }

    void Start()
    {
        
        SpawnObject();
    }
	
}

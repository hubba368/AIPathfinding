using UnityEngine;
using System.Collections;

public class CreateWorld : MonoBehaviour
{
    public Vector3 spawnPoint;
    public Quaternion rotation;

    public GameObject[] actors; //array of actors to allow for diff models to be generated
    public int maxActors;  //max amount that can be spawned
    public float spawnSpeed; //diff speeds of generation for each spawner gameObject

    public bool isComplete = false;

    Vector3 GetRandomSpawnPoint()
    {
        //Generate a random spawn point by using insideUnitSphere
        Vector3 spawnRadius = Random.insideUnitSphere * 74f;
        spawnRadius.y = 0.0f;

        spawnPoint = spawnRadius + transform.position;

        return spawnPoint;
    }

    IEnumerator SpawnObject()
    {
        //Spawn objects that are set in the inspector
        int spawned = 0;
        float direction = Random.Range(0f, 360f);  //get a random object rotation

        while(spawned < maxActors)
        {
            //spawn until limit reached
            int prefab = Random.Range(0, 3);
            Instantiate(actors[prefab], GetRandomSpawnPoint(), 
                Quaternion.Euler(new Vector3(0f, direction, 0f)));
            spawned++;
            yield return new WaitForSeconds(spawnSpeed);
        }

        if(spawned == maxActors)
        {
            //not implemented - would cause actors to be spawned 
            isComplete = true;
        }
    }

    void Start()
    {      
        StartCoroutine(SpawnObject());
    }
	
}

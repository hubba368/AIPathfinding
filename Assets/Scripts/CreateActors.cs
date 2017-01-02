using UnityEngine;
using System.Collections;

public class CreateActors : MonoBehaviour
{

    public Vector3 spawnPoint;
    public Quaternion rotation;

    public GameObject[] actors;
    public int maxActors;  //max amount that can be spawned



    Vector3 GetRandomSpawnPoint()
    {
        //Generate a random spawn point by using insideUnitSphere
        Vector3 spawnRadius = Random.insideUnitSphere * 74f;
        spawnRadius.y = 1.0f;

        spawnPoint = spawnRadius + transform.position;

        return spawnPoint;
    }

    IEnumerator SpawnObject()
    {
        //Spawn objects that are set in the inspector
        int spawned = 0;
        float direction = Random.Range(0f, 360f);

        GameObject check = GameObject.Find("TreeSpawner");
        CreateWorld checkIfComplete = check.GetComponent<CreateWorld>();
        if (checkIfComplete.isComplete == true)
        {
            while (spawned < maxActors)
            {
                //spawn until limit reached
                int prefab = Random.Range(0, 3);
                Instantiate(actors[prefab], GetRandomSpawnPoint(), 
                    Quaternion.Euler(new Vector3(0f, direction, 0f)));
                spawned++;

                yield return new WaitForSeconds(0.5f);
            }
            checkIfComplete.isComplete = false;
        }
    }

    void Update()
    {
        StartCoroutine(SpawnObject());   
    }

}

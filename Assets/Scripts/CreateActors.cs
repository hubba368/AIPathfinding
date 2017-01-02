using UnityEngine;
using System.Collections;

public class CreateActors : MonoBehaviour
{

    public Vector3 spawnPoint;
    public Quaternion rotation;

    public GameObject[] actors;

    public int maxActors = 4;  //max amount that can be spawned
    public float spawnSpeed;



    Vector3 GetRandomSpawnPoint()
    {
        //Generate a random spawn point by using insideUnitSphere
        Vector3 spawnRadius = Random.insideUnitSphere * 74f;
        spawnRadius.y = 1.0f;   //have actor y value at 1f to stop clipping and subsequent falling through terrain on spawn

        spawnPoint = spawnRadius + transform.position;

        return spawnPoint;
    }

    IEnumerator SpawnObject()
    {
        //Spawn objects that are set in the inspector
        int spawned = 0;
        float direction = Random.Range(0f, 360f);

      //  GameObject check = GameObject.Find("TreeSpawner");
      //  CreateWorld checkIfComplete = check.GetComponent<CreateWorld>();

        //Tried making it so actors would spawn when all plants were spawned.
        //Could get it to work but only if the coroutine was started in an update method,
        //which would cause a large amount of actors to be spawned.
        while (spawned < maxActors)
        {
            //spawn until limit reached
            int prefab = Random.Range(0, 3);
            Instantiate(actors[prefab], GetRandomSpawnPoint(),
                Quaternion.Euler(new Vector3(0f, direction, 0f)));
            spawned++;

            yield return new WaitForSeconds(spawnSpeed);

        }
   }

    void Start()
    {
        StartCoroutine(SpawnObject());   
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public Vector3 randomWayPoint;

    public List<Transform> visibleTargets = new List<Transform>();  //list of visible targets

    public float speed = 1f; //speed of actor

    public float wanderTimer;
    public float timer;

    public NavMeshAgent agent;
   // public float hitRange = 1f;
    


    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .1f);
        StartCoroutine("Wander", 5f);
    }

    //coroutine to make detecting targets not instantaneous
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    //coroutine to make wandering not instant - gets rid of character shaking constantly
    IEnumerator Wander(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            NewDirection();
        }
    }




    //find targets in view radius
    void FindVisibleTargets()
    {
        visibleTargets.Clear();  //reset target list

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);  //get array of all colliders in view radius

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            //Get the location of the target
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = target.position - transform.position;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                //get the distance to the target
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    //if not blocked, add to target list
                    visibleTargets.Add(target);
                    
                }
            }
        }
    }

    //called every frame - currently controls actor movement
    void FixedUpdate()
    {
        //originally used array of colliders, but it would try and move towards trees that are blocked
        //Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        Transform closest;
        Vector3 tgt;


        //check if we have any targets
        if (visibleTargets.Count > 0)
        {
            //get the first target i.e. the closest
            closest = visibleTargets[0];

            //for each target...
            foreach (Transform obj in visibleTargets)
            {
                if (obj == null)
                {
                    //small check incase a tree is destroyed by another human
                    return;
                }
                float nextObj = Vector3.Distance(obj.transform.position, transform.position);
                float closestObj = Vector3.Distance(closest.transform.position, transform.position);

                //if the new target is closer than the first target, move to it
                if (nextObj < closestObj)
                {
                    tgt = obj.transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, tgt, speed);
                }
                else
                {
                    //else move to closest target
                    tgt = closest.transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, tgt, speed);
                }

                if (nextObj == closestObj)
                {
                    // if the distances are the same, go to the target that was seen first
                    tgt = closest.transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, tgt, speed);
                }
            }
        }
        else if (visibleTargets.Count == 0)
        {
            //if no targets, wander forever
            transform.position = Vector3.MoveTowards(transform.position, randomWayPoint, speed);
        }         
    }

    void NewDirection()
    {
        //find a random point
        Vector3 offset = Random.insideUnitSphere * 500f;
        randomWayPoint = transform.position + offset;
    }



    void OnCollisionEnter(Collision coll)
    {
        //if 'human' collides with 'tree' destroy it
        if (coll.gameObject.layer == 9)
        {
            Destroy(coll.gameObject);
            visibleTargets.Remove(coll.transform);  //forgot to remove destroyed object from list lol
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

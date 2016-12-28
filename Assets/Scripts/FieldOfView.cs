using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 361)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;   


    public List<Transform> visibleTargets = new List<Transform>();  //list of targets

    public float speed = 1f; //speed of actor
   // public float hitRange = 1f;
    





    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
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

    //find targets in view radius
    void FindVisibleTargets()
    {
        visibleTargets.Clear();  //reset target list

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);  //get array of all colliders in view radius

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            //Get the location of the target
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

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
        //get array of all colliders in radius
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        Collider closest;
        Vector3 tgt;

        //check if we have any targets
        if (targetsInViewRadius.Length > 0)
        {
            //get the first target i.e. the closest
            closest = targetsInViewRadius[0];

            //for each target...
            foreach (Collider obj in targetsInViewRadius)
            {
                //if the new target is closer than the first target, move to it
                if (Vector3.Distance(obj.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position))
                {
                    tgt = obj.transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, tgt, speed);
                }
                else
                {
                    //move to closest target
                    tgt = closest.transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, tgt, speed);
                }
            }
        }

        
            
    }

    void OnCollisionEnter(Collision coll)
    {
        //if 'human' collides with 'tree' destroy it
        if (coll.gameObject.layer == 9)
        {
            Destroy(coll.gameObject);

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

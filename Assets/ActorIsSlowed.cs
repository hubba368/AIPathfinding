using UnityEngine;
using System.Collections;

public class ActorIsSlowed : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
    {
        //needed to allow for clipping between moving actors
        //could add slow down effect when characters walk through small plants??
    }
}

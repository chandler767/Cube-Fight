using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Rotates a object at a random rate. 

public class Rotator : MonoBehaviour {
	void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 0.3f;
    }
}

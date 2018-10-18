using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
	void Start()
    {
        var rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * 0.3f;
    }
}

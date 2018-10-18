using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class eyeposition : MonoBehaviour {
	public MeshRenderer meshRenderer;


	// Use this for initialization
	void Start () {
		MLEyes.Start();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();	
	}
	
	// Update is called once per frame
	void Update () {
		if (MLEyes.IsStarted) {
         	meshRenderer.transform.position = MLEyes.FixationPoint;
		}
		
	}

	void OnDestroy () {
        MLEyes.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
// This scripts moves a game object to the fixation point of the users eyes. 

public class EyeFocus : MonoBehaviour {
	public MeshRenderer meshRenderer;

	// Start eye tracking.
	void Start () {
		MLEyes.Start();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();	
	}
	
	// Update is called once per frame.
	void Update () {
		if (MLEyes.IsStarted) {
         	meshRenderer.transform.position = MLEyes.FixationPoint;
		}
	}

	// Stop tracking.
	void OnDestroy () {
        MLEyes.Stop();
    }
}
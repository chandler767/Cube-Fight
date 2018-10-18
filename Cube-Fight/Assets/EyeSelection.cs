using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using PubNubAPI;
// Tracks the fixation point of the users eyes and selects a game object when user gazes at it.
// When the controller trigger is pulled a message is published to a channel that is used to determine the ownership of the game object.


public class EyeSelection : MonoBehaviour {
    public GameObject Camera;
    public Material FocusedMaterial, NonFocusedMaterial, OwnedMaterial; // User looking, user not looking, and user owns. 
    public static PubNub pubnub;

    private MeshRenderer meshRenderer; // The game object.
    private bool looking; // Is the user looking at the game object?
    private bool owned; // Does the user own the game object?
    private string pn_uuid; // The UUID is unique to the PubNub client used for each game object and is used to determine who owns the game object.

    private Vector3 _heading; // Where the user looking.

    void Start () {
        MLEyes.Start(); // Start eye tracking.
        MLInput.Start(); // Start input controller.
        MLInput.OnTriggerDown += HandleOnTriggerDown; // Get trigger down event.

        meshRenderer = gameObject.GetComponent<MeshRenderer>(); // Get the game object.

        PNConfiguration pnConfiguration = new PNConfiguration(); // Start PubNub 
        pnConfiguration.PublishKey = "pub-c-bb13912e-7007-46ce-a954-74fe4c6cb131"; // YOUR PUBLISH KEY HERE. 
        pnConfiguration.SubscribeKey = "sub-c-e14cda48-b857-11e8-b27d-1678d61e8f93"; // YOUR SUBSCRIBE KEY HERE.
        pubnub = new PubNub(pnConfiguration);
        pn_uuid = pnConfiguration.UUID; // Get the UUID of the PubNub client.

        pubnub.Subscribe()
            .Channels(new List<string>(){
                meshRenderer.name // Subscribe to the channel for the game object. 
            })
            .Execute();
        pubnub.SusbcribeCallback += (sender, e) => { 
            SusbcribeEventEventArgs message = e as SusbcribeEventEventArgs;
            if (message.Status != null) {
                    switch (message.Status.Category) {
                    case PNStatusCategory.PNUnexpectedDisconnectCategory:
                    case PNStatusCategory.PNTimeoutCategory:
                        pubnub.Reconnect();
                    break;
                }
            }
            if (message.MessageResult != null) {
                // Does the message equal the UUID for this client? 
                if (message.MessageResult.Payload.ToString() == pn_uuid) { // Message and client UUID are the same. 
                    meshRenderer.material = OwnedMaterial; // The user owns the game object, change material to OwnedMaterial to show.
                    looking = false;
                    owned = true;
                } else { // Message and client UUID are NOT the same. 
                    if (owned) { // Only need to change color if the user owns the game object.
                        meshRenderer.material = NonFocusedMaterial; // Another user has taken the game object, change material to NonFocusedMaterial to show..
                        owned = false;
                    }
                }
            }
        };
    }    
    void OnDestroy () { // Stop eye tracking and inputs.
        MLEyes.Stop();
        MLInput.OnTriggerDown -= HandleOnTriggerDown;
        MLInput.Stop();
    }

    void Update () {
        if (MLEyes.IsStarted) {
            RaycastHit rayHit = new RaycastHit();
            _heading = MLEyes.FixationPoint - Camera.transform.position;
            if (Physics.Raycast(Camera.transform.position, _heading, out rayHit, 10.0f)) { // Check for collisions of user's eye gaze with a game object.
                if (rayHit.collider.name == meshRenderer.name) { // Check if collision is with this game object.
                    if (!owned) { // Only highlight if the user does not own the game object.
                        meshRenderer.material = FocusedMaterial;  
                        looking = true;
                    }
                }
            }
            else { // User is not looking.
                if (!owned) { // Only remove highlight if the user does not own the game object.
                    meshRenderer.material = NonFocusedMaterial; 
                    looking = false;
                }
            }   
        }
    }

    void HandleOnTriggerDown(byte controllerId, float value) {
        if (looking) { // Send a message to take the game object if the user is looking and the trigger was pulled.
            pubnub.Publish()
                .Channel(meshRenderer.name) // Send a message to the channel for the game object.
                .Message(pn_uuid) // Send the UUID for this client.
                .Async((result, status) => {    
                    if (!status.Error) {
                        Debug.Log(string.Format("Publish Timetoken: {0}", result.Timetoken));
                    } else {
                        Debug.Log(status.Error);
                        Debug.Log(status.ErrorData.Info);
                    }
                });
        }
    }
}
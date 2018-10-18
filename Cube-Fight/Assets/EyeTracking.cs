using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using PubNubAPI;

public class EyeTracking : MonoBehaviour {
    public GameObject Camera;
    public Material FocusedMaterial, NonFocusedMaterial, OwnedMaterial;
    public MeshRenderer meshRenderer;
    public bool looking;
    public bool owned;
    public static PubNub pubnub;
    public string pn_uuid;

    private Vector3 _heading;

    void Start () {
        MLEyes.Start();
        MLInput.Start();
        MLInput.OnTriggerDown += HandleOnTriggerDown;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();

        PNConfiguration pnConfiguration = new PNConfiguration();
        pnConfiguration.PublishKey = "pub-c-bb13912e-7007-46ce-a954-74fe4c6cb131";
        pnConfiguration.SubscribeKey = "sub-c-e14cda48-b857-11e8-b27d-1678d61e8f93";
        pnConfiguration.Secure = true;
        pubnub = new PubNub(pnConfiguration);
        pn_uuid = pnConfiguration.UUID;
        pubnub.Subscribe()
            .Channels(new List<string>(){
                meshRenderer.name
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
                if (message.MessageResult.Payload.ToString() == pn_uuid) {
                    meshRenderer.material = OwnedMaterial; 
                    looking = false;
                    owned = true;
                } else {
                    if (owned) {
                        meshRenderer.material = NonFocusedMaterial; 
                        owned = false;
                    }
                }
            }
        };
    }    
    void OnDestroy () {
        MLEyes.Stop();
        MLInput.OnTriggerDown -= HandleOnTriggerDown;
        MLInput.Stop();
    }

    void Update () {
        if (MLEyes.IsStarted) {
            RaycastHit rayHit = new RaycastHit();
            _heading = MLEyes.FixationPoint - Camera.transform.position;
            if (Physics.Raycast(Camera.transform.position, _heading, out rayHit, 10.0f)) {
                if (rayHit.collider.name == meshRenderer.name) {
                    if (!owned) {
                        meshRenderer.material = FocusedMaterial;
                        looking = true;
                    }
                }
            }
            else {
                if (!owned) {
                    meshRenderer.material = NonFocusedMaterial; 
                    looking = false;
                }
            }   
        }
    }

    void HandleOnTriggerDown(byte controllerId, float value) {
        if (looking) {
            pubnub.Publish()
                .Channel(meshRenderer.name)
                .Message(pn_uuid)
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
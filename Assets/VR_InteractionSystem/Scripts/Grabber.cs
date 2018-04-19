using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Grabber : MonoBehaviour {
    [SerializeField]
    ControllerEventHandler handler;

    FixedJoint joint;
    GameObject GrabbableObject;

	// Use this for initialization
	void Start () {
	    if(handler == null)
        {
            handler = GetComponent<ControllerEventHandler>();
        }

        joint = GetComponent<FixedJoint>();

        handler.OnControllerAction
            .Where(x => x == "PressGrabButton")
            .Subscribe(x => Grab());
        handler.OnControllerAction
            .Where(x => x == "ReleaseGrabButton")
            .Subscribe(x => Release());


	}
	
	// Update is called once per frame
	void Update () {
	}

    void Grab()
    {
        if (GrabbableObject == null || joint.connectedBody != null)
        {
            return;
        }

        joint.connectedBody = GrabbableObject.GetComponent<Rigidbody>();
        GrabbableObject.GetComponent<GrabbableObject>().OnGrab();
    }

    void Release()
    {
        if (joint.connectedBody == null)
        {
            return;
        }

        Rigidbody rigidBody = joint.connectedBody;
        joint.connectedBody = null;

        var device = handler.Device;
        rigidBody.velocity = device.velocity;
        rigidBody.angularVelocity = device.angularVelocity;
        rigidBody.maxAngularVelocity = rigidBody.angularVelocity.magnitude;
        rigidBody.gameObject.GetComponent<GrabbableObject>().OnRelease();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<GrabbableObject>())
        {
            GrabbableObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == GrabbableObject)
        {
            GrabbableObject = null;
        }
    }
}

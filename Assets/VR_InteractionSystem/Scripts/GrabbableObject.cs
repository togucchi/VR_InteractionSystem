using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrabbableObject : MonoBehaviour {

    Item itemScript;

	// Use this for initialization
	protected virtual void Start () {
        itemScript = GetComponent<Item>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

    public virtual void OnGrab()
    {
    }

    public virtual void OnRelease()
    {
        itemScript.Throw();
    }
}

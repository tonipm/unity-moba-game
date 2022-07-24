using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBase : Action {

	// Use this for initialization
	void Start () {
        this.timeOut = 5f;
	}
	
    protected override void Tick()
    {

    }
}

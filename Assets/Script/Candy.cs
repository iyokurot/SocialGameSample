﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		this.transform.Translate (0, 0, 0.5f);
	}
	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other);
	}
}
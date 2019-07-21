using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnCollision : MonoBehaviour {
	GameObject controller;

	// Use this for initialization
	void Start () {
		controller=GameObject.Find("RunningController");
	}
	
	//キャラ衝突
	void OnTriggerEnter(Collider collider){
		controller.GetComponent<RunningController>().EnemyCollision();
		Destroy(this.gameObject);
	}
}

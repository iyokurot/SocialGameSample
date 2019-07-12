using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelSelectController : MonoBehaviour {
	public int jewelcost;
	private GameObject controller;

	// Use this for initialization
	void Start () {
		controller=GameObject.Find("Header");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void onClickBuy(){
		//question.SetActive(true);
		controller.GetComponent<HeaderController>().buyjewel(jewelcost);
	}
}

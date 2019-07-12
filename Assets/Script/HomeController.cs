using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HomeController : MonoBehaviour {
	public GameObject character;
	private Animator animater;

	/*
	public Text username;
	public Text level;
	public Text jewel;
	public Text coin;

	public GameObject jewelList;
	public GameObject question;
	private int jewelcost=0;
	*/

	// Use this for initialization
	void Start () {
		animater=character.GetComponent<Animator>();
		/*
		username.text=UserDataLoader.userdata.name;
		level.text=UserDataLoader.userdata.level.ToString();
		SetJewels();
		coin.text=UserDataLoader.userdata.coin.ToString();
		*/
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		if(EventSystem.current.IsPointerOverGameObject()){
			return;
			}
		#else 
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
            return;
        }
        #endif

		if(Input.GetMouseButton(0)){
			//animater.SetBool("Jumping",true);
			animater.SetTrigger("JumpingTrigger");
		}
	}
	/*
	private void SetJewels(){
		jewel.text=UserDataLoader.userdata.jewel.ToString();
	}
	

	public void onClickBuyjewels(){
		jewelList.SetActive(true);
	}
	*/
	public void onClicktoGatya(){
		SceneManager.LoadScene("GatyaTop");
	}
/*
	public void buyjewel(int jewels){
		question.SetActive(true);
		jewelcost=jewels;
	}

	public void onClickBuyYes(){
		//課金
		UserDataLoader.userdata.jewel+=jewelcost;
		question.SetActive(false);
		SetJewels();
	}
	public void onClickBuyNo(){
		question.SetActive(false);
	}
	*/

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeaderController : MonoBehaviour {

	public Text username;
	public Text level;
	public Text jewel;
	public Text coin;

	public GameObject jewelList;
	public GameObject question;
	public GameObject menu;
	private int jewelcost=0;
	

	// Use this for initialization
	void Start () {
		username.text=UserDataLoader.userdata.name;
		level.text=UserDataLoader.userdata.level.ToString();
		SetJewels();
		coin.text=UserDataLoader.userdata.coin.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void SetJewels(){
		jewel.text=UserDataLoader.userdata.jewel.ToString();
	}

	//メニューを開く
	public void onClickMenu(){
		menu.SetActive(true);
	}
	public void onClickBuyjewels(){
		jewelList.SetActive(true);
	}

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

	//メニューを閉じる
	public void onClickCloseMenu(){
		menu.SetActive(false);
	}

	//タイトルへ
	public void onClickBackTitle(){
		SceneManager.LoadScene("Title");
	}

}

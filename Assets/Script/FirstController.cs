using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstController : MonoBehaviour {
	public InputField namefield;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClickEnter(){
		if(namefield.text==""){
			return;
		}
		Userdata userdata=new Userdata();
		userdata.name=namefield.text;
		userdata.jewel=0;
		userdata.coin=0;
		userdata.level=1;
		userdata.character="cecil_sharo";
		UserDataLoader.userdata=userdata;
		//保存
		UserDataLoader.writeData();
		LoadScene();
	}

	void LoadScene(){
		SceneManager.LoadScene("Home");
	}
}

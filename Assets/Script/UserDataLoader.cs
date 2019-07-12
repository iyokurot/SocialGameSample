using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataLoader : MonoBehaviour {
	public static Userdata userdata;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void test(){
		Debug.Log("tester!");
	}

	public static void loadData(string str){
		userdata=JsonUtility.FromJson<Userdata>(str);
	}

	public static void writeData(){
		/* 
		Userdata userdata=new Userdata();
        userdata.name="testuser";
		userdata.jewel=10000;
		userdata.coin=100000;

        string jsonstr = JsonUtility.ToJson (userdata);
        jsonController.writeJson(jsonstr,"/userdata.json");
		*/
		JsonController jc=new JsonController();
		string jsonstr = JsonUtility.ToJson (userdata);
		jc.writeJson(jsonstr,"/userdata.json");

	}


	private void OnApplicationPause (bool pauseStatus){
	//一時停止
	if(pauseStatus){
		writeData();
	}
	//再開時
	else{

	}
	}
	//アプリ終了時
	private void OnApplicationQuit (){
		writeData();
	}

}

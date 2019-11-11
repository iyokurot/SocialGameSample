using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstController : MonoBehaviour {
	public InputField namefield;
	JsonController jsoncontroller = new JsonController ();

	// Use this for initialization
	void Start () {
		//所持キャラデータJson生成
		createCharaJson ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void onClickEnter () {
		if (namefield.text == "") {
			return;
		}
		Userdata userdata = new Userdata ();
		userdata.name = namefield.text;
		userdata.jewel = 0;
		userdata.coin = 0;
		userdata.level = 1;
		userdata.character = 1;
		UserDataLoader.userdata = userdata;
		//保存
		UserDataLoader.writeData ();
		LoadScene ();
	}
	void createCharaJson () {
		List<JCharacterData> charalist = new List<JCharacterData> ();
		JCharacterData newChara = new JCharacterData ();
		newChara.id = 1;
		newChara.level = 1;
		newChara.skillLevel = 1;
		charalist.Add (newChara);
		string jsonstr = JsonUtility.ToJson (new Serialize<JCharacterData> (charalist));
		jsoncontroller.writeJson (jsonstr, "/characterdata.json");
	}

	void LoadScene () {
		SceneManager.LoadScene ("Home");
	}
}
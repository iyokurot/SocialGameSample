using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeamCharaController : MonoBehaviour {
	JsonController jsoncontroller = new JsonController ();
	List<JCharacterData> charalist = new List<JCharacterData> (); //所持キャラリスト
	int charaId;
	[SerializeField]
	CharacterDataBase characterDataBase;
	CharacterData character;
	[SerializeField]
	GameObject teamPanel; //編成パネル
	[SerializeField]
	GameObject havePanel; //選択パネル
	int charalevel = 5;
	int skilllevel = 1;
	class levelUper {
		public static int HPup = 100;
		public static int Mat = 10;
		public static int Bat = 20;
		public static int skilup = 10;
	};

	// Use this for initialization
	void Start () {
		charaId = 1; //UserDataLoader.userdata.character;
		loadCharacter ();
		SetComponents ();
	}

	// Update is called once per frame
	void Update () {
		if (UserDataLoader.userdata.character != charaId) {
			charaId = UserDataLoader.userdata.character;
			havePanel.SetActive (false);
			loadCharacter ();
			SetComponents ();
		}
	}
	private void loadCharacter () {
		//IDから取得
		character = GetCharacter (charaId);
		charalist = readJson ();
		JCharacterData loadchara = charalist.Find (character => character.id == charaId);
		charalevel = loadchara.level;
		skilllevel = loadchara.skillLevel;

	}
	private CharacterData GetCharacter (int id) {
		return characterDataBase.GetList ().Find (chara => chara.GetId () == id);
	}
	//取得キャラ読み込み
	private List<JCharacterData> readJson () {
		string datastr = jsoncontroller.readJsonChangeable ("/characterdata.json");
		return JsonUtility.FromJson<Serialize<JCharacterData>> (datastr).ToList ();
	}

	//Panelの各コンポーネントへセット
	private void SetComponents () {
		//アイコン
		teamPanel.transform.FindChild ("CharaImage").GetComponent<Image> ().sprite = character.GetMiniSprite ();
		//名前＆サブ
		teamPanel.transform.FindChild ("CharaName").GetComponent<Text> ().text = "[" + character.GetSubTitle () + "]" + character.GetCharaName ();

		//Detailパネル
		var detailPanel = teamPanel.transform.FindChild ("Detail");
		//Levelテキスト
		detailPanel.transform.FindChild ("Level").GetComponent<Text> ().text = "Lv." + charalevel;
		//HPテキスト
		int plusHP = (levelUper.HPup * charalevel) + character.GetHP ();
		detailPanel.transform.FindChild ("HP").GetComponent<Text> ().text = "HP " + plusHP;
		//miniATテキスト
		int plusminiAT = (levelUper.Mat * charalevel) + character.GetSmallAT ();
		detailPanel.transform.FindChild ("miniAT").GetComponent<Text> ().text = "miniAT " + plusminiAT;
		//bigATテキスト
		int plusbigAT = (levelUper.Bat * charalevel) + character.GetBigAT ();
		detailPanel.transform.FindChild ("bigAT").GetComponent<Text> ().text = "bigAT " + plusbigAT;

		//スキルパネル
		var skillPanel = teamPanel.transform.FindChild ("SkillPanel");
		//スキル画像
		skillPanel.transform.FindChild ("Image").GetComponent<Image> ().sprite = character.GetSkillImage ();
		//スキル名
		skillPanel.transform.FindChild ("SkillName").GetComponent<Text> ().text = character.GetSkillName () + "(" + skilllevel + ")";
		//スキル効果
		//レベルによる計算<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		skillPanel.transform.FindChild ("SkillEffect").GetComponent<Text> ().text = character.GetSkillEffect ();
	}

	//戻るボタン
	public void OnClickBackScene () {
		SceneManager.LoadScene ("CharacterMode");
	}
}
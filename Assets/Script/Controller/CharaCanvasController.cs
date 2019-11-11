using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//所持キャラクター表示Prefab用クラス
public class CharaCanvasController : MonoBehaviour {
	JsonController jsoncontroller = new JsonController ();
	List<JCharacterData> charalist = new List<JCharacterData> (); //所持キャラリスト
	[SerializeField]
	GameObject contentObject;
	[SerializeField]
	GameObject charaPanel;
	[SerializeField]
	CharacterDataBase characterDataBase;
	[SerializeField]
	GameObject viewPanel;
	List<CharacterData> haveList = new List<CharacterData> ();
	string name;
	// Use this for initialization
	void Start () {
		createList ();
		foreach (var chara in haveList) {
			charaPanel.GetComponent<Image> ().sprite = chara.GetMiniSprite ();
			charaPanel.GetComponent<ContentCharaData> ().SetData (chara.GetId ());
			GameObject panel = Instantiate (charaPanel, contentObject.transform);
		}
	}

	// Update is called once per frame
	void Update () {

	}
	//取得キャラ読み込みのちlist作成
	private void createList () {
		charalist = readJson ();
		foreach (var haveChara in charalist) {
			haveList.Add (GetCharacter (haveChara.id));
		}
	}
	private CharacterData GetCharacter (int id) {
		return characterDataBase.GetList ().Find (chara => chara.GetId () == id);
	}
	//取得キャラ読み込み
	private List<JCharacterData> readJson () {
		string datastr = jsoncontroller.readJsonChangeable ("/characterdata.json");
		return JsonUtility.FromJson<Serialize<JCharacterData>> (datastr).ToList ();
	}
}
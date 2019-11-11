using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllCharaController : MonoBehaviour {
	GameObject contentObject;
	[SerializeField]
	GameObject charaPanel;
	[SerializeField]
	CharacterDataBase charas;
	[SerializeField]
	GameObject viewPanel;
	// Use this for initialization
	void Start () {
		contentObject = GameObject.Find ("Content");

		for (int i = 0; i < 99; i++) {
			//GameObject chara = Instantiate (charaPanel, contentObject.transform);
			//chara.transform.parent = contentObject.transform;
		}
		foreach (var chara in charas.GetList ()) {
			charaPanel.GetComponent<Image> ().sprite = chara.GetMiniSprite ();
			charaPanel.GetComponent<ContentCharaData> ().SetData (chara.GetId ());
			GameObject panel = Instantiate (charaPanel, contentObject.transform);
		}
	}

	// Update is called once per frame
	void Update () {

	}
	public void ViewActive (int id) {
		Debug.Log (id);
		//名前から情報検索のちView表示
		CharacterData character = GetCharacter (id);
		viewPanel.transform.FindChild ("Image").GetComponent<Image> ().sprite = character.GetBigSprite ();
		viewPanel.SetActive (true);
	}
	private CharacterData GetCharacter (int id) {
		return charas.GetList ().Find (chara => chara.GetId () == id);
	}

	//戻るボタン
	public void OnClickBackScene () {
		SceneManager.LoadScene ("CharacterMode");
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCharaData : MonoBehaviour {
	//キャラ一覧表示にて各コンテントにアタッチ
	//IDを保持
	public int setId = 0;
	private int charaId = 0;
	public void SetData (int id) {
		setId = id;
	}
	public int GetData () {
		return charaId;
	}
	void Update () {
		if (charaId != setId) {
			charaId = setId;
		}
	}
}
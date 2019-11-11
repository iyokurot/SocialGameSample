using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ClickContent : MonoBehaviour, IPointerClickHandler {
	//ScrollViewのContentにアタッチ、項目クリックEventTrigger
	[SerializeField]
	GameObject controller;
	public void OnPointerClick (PointerEventData eventData) {
		//クリックされたオブジェクトからキャラIDを取得
		int modelname = eventData.pointerEnter.GetComponent<ContentCharaData> ().GetData ();
		controller.GetComponent<AllCharaController> ().ViewActive (modelname);
	}
}
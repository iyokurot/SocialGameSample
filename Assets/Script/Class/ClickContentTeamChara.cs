using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ClickContentTeamChara : MonoBehaviour, IPointerClickHandler {
	public void OnPointerClick (PointerEventData eventData) {
		//クリックされたオブジェクトからキャラモデル名を取得
		int modelname = eventData.pointerEnter.GetComponent<ContentCharaData> ().GetData ();
		//UserDataに登録
		UserDataLoader.userdata.character = modelname;
	}
}
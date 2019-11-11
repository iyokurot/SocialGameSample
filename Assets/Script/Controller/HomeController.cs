using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeController : MonoBehaviour {
	private Animator animater;

	private GameObject character;
	[SerializeField]
	private CharacterDataBase characterDataBase;

	// Use this for initialization
	void Start () {
		loadCharacter ();
		animater = character.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		if (EventSystem.current.IsPointerOverGameObject ()) {
			return;
		}
#else 
		if (EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId)) {
			return;
		}
#endif

		if (Input.GetMouseButton (0)) {
			//animater.SetBool("Jumping",true);
			animater.SetTrigger ("JumpingTrigger");
		}
	}

	//ホームキャラロード
	private void loadCharacter () {
		CharacterData homechara = GetCharacter (UserDataLoader.userdata.character);
		character = GameObject.Instantiate (homechara.GetModel ()) as GameObject;

	}
	private CharacterData GetCharacter (int id) {
		return characterDataBase.GetList ().Find (chara => chara.GetId () == id);
	}

	public void onClicktoGatya () {
		SceneManager.LoadScene ("GatyaTop");
	}

	public void onClicktoRunGame () {
		SceneManager.LoadScene ("RunningGame");
	}

	public void onClicktoVoiceGame () {
		SceneManager.LoadScene ("VoiceGame");
	}

}
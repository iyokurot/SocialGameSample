using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HomeController : MonoBehaviour {
	private Animator animater;

	private GameObject character;

	// Use this for initialization
	void Start () {
		loadCharacter();
		animater=character.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		if(EventSystem.current.IsPointerOverGameObject()){
			return;
			}
		#else 
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
            return;
        }
        #endif

		if(Input.GetMouseButton(0)){
			//animater.SetBool("Jumping",true);
			animater.SetTrigger("JumpingTrigger");
		}
	}

	//ホームキャラロード
	private void loadCharacter(){
		var homechara=UserDataLoader.userdata.character;//"cecil_sharo";
		var chara=Resources.Load("chara/"+homechara) as GameObject;
		character=GameObject.Instantiate(chara) as GameObject;
		
	}

	public void onClicktoGatya(){
		SceneManager.LoadScene("GatyaTop");
	}

	public void onClicktoRunGame(){
		SceneManager.LoadScene("RunningGame");
	}

	public void onClicktoVoiceGame(){
		SceneManager.LoadScene("VoiceGame");
	}

}

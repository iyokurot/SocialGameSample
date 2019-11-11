using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FooterController : MonoBehaviour {
	string sceneName;
	void Start () {
		sceneName = SceneManager.GetActiveScene ().name;
	}
	public void ToHome () {
		if ("Home" == sceneName) {
			return;
		}
		SceneManager.LoadScene ("Home");
	}
	public void ToChara () {
		if ("CharacterMode" == sceneName) {
			return;
		}
		SceneManager.LoadScene ("CharacterMode");
	}
	public void ToBattle () {
		if ("BattleMode" == sceneName) {
			return;
		}
		SceneManager.LoadScene ("BattleMode");
	}
	public void ToHouse () {
		//
	}
	public void ToGatya () {
		if ("GatyaTop" == sceneName) {
			return;
		}
		SceneManager.LoadScene ("GatyaTop");
	}

}
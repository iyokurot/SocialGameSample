using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterModeController : MonoBehaviour {
	public void OnClickParty () {
		SceneManager.LoadScene ("TeamCharas");
	}
	public void OnClickAllmenber () {
		SceneManager.LoadScene ("AllCharas");
	}
}
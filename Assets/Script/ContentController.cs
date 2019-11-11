using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ContentController : MonoBehaviour {
	public void ClickVoiceMode () {
		SceneManager.LoadScene ("VoiceGame");
	}
	public void ClickRunMode () {
		SceneManager.LoadScene ("RunningGame");
	}
}
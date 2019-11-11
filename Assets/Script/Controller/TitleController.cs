using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {
	JsonController jsonController = new JsonController ();

	public Text tap;
	private float time = 0;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		updateText ();
		if (Input.GetMouseButton (0)) {
			//userdataロード
			string datastr = jsonController.readJsonChangeable ("/userdata.json");
			UserDataLoader.loadData (datastr);
			//シーンLoad
			LoadScene ();
		}
	}

	void LoadScene () {
		if (UserDataLoader.userdata == null) {
			jsonController.readJsonChangeable ("/characterdata.json");
			SceneManager.LoadScene ("UserDataFirst");
		} else {
			SceneManager.LoadScene ("Home");
		}
	}

	private void updateText () {
		var color = tap.color;
		time += Time.deltaTime * 5.0f * 0.5f;
		color.a = Mathf.Sin (time) * 0.5f + 0.5f;
		tap.color = color;
	}
}
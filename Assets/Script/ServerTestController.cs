using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ServerTestController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("start");
		StartCoroutine (GetServerData ());

		StartCoroutine (PostServerData ());
	}

	// Update is called once per frame
	void Update () {

	}
	//Get通信テスト
	IEnumerator GetServerData () {
		UnityWebRequest req = UnityWebRequest.Get ("http://localhost:3000/Get");
		req.SetRequestHeader ("key", "KEY");
		yield return req.Send ();
		if (req.isError) {
			Debug.Log (req.error);
		} else {
			if (req.responseCode == 200) {
				//OK
				string jsonText = req.downloadHandler.text;
				List<Testdata> list = new List<Testdata> ();
				list = JsonUtility.FromJson<Serialize<Testdata>> (jsonText).ToList ();
				Debug.Log (list[0].name);
			}
		}
	}
	//Post通信テスト
	IEnumerator PostServerData () {
		WWWForm form = new WWWForm ();
		//key:data
		form.AddField ("myField", "myData");
		form.AddField ("newxt", 20);

		using (UnityWebRequest req = UnityWebRequest.Post ("http://localhost:3000/Post", form)) {
			yield return req.Send ();

			if (req.isError) {
				Debug.Log (req.error);
			} else {
				Debug.Log ("Form upload complete!");
			}
		}
	}

	[System.Serializable]
	public class Testdata {
		public string id;
		public string name;
	}
}
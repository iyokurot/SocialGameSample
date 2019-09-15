using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VoiceController : MonoBehaviour {

	[SerializeField]
	private GameObject camera;
	public GameObject poseMenu;

	[SerializeField]
	private Text timeText;
	float alltime = 0;
	[SerializeField]
	private Text scoreText;
	int score = 0;
	int breakenemynum = 0; //撃破敵数

	private Animator animater;
	private GameObject character;
	private CharacterController charaController;
	public GameObject charaplace;

	public Text test;

	private bool lockon = false;
	private List<GameObject> lockEnemys = new List<GameObject> ();

	[SerializeField, Range (0f, 10f)] float m_gain = 1f; // 音量に掛ける倍率
	float m_volumeRate; // 音量(0-1)
	public GameObject volumeBar;
	public GameObject hpBar;
	bool isClear = false; //クリア判定
	bool clearResult = true;
	bool isDead = false; //ゲームオーバー判定
	[SerializeField]
	GameObject resultCanvas;
	[SerializeField]
	GameObject tapText;
	bool isScore = false;

	//Stage loadings テスト
	float limittime = 120.0f; //制限時間
	int enemynum = 1; //出現敵数
	int[] enemydata = { 0, 0, 1, 0, 0 }; //出現敵データ
	int boss = 0; //出現ボスid

	//Player loadings テスト
	string charamodelname = "cecil_sharo"; //モデル名
	int charaHPMax = 1000;
	int charaHP = 1000; //体力
	int smallAT = 100; //小攻撃
	int bigAT = 500; //大攻撃
	//skill

	//Enemy loadings テスト
	Enemydata[] enemys = {
		new Enemydata ("normal", 0, 500, 100),
		new Enemydata ("magic", 1, 500, 100)
	};

	int nowenemynum = 0; //残り出現敵数
	float enemyappercount = 0.0f;

	void Start () {
		loadCharacter ();
		animater = character.GetComponent<Animator> ();
		charaController = character.GetComponent<CharacterController> ();
		animater.SetBool ("Stay", true);

		AudioSource aud = GetComponent<AudioSource> ();
		if ((aud != null) && (Microphone.devices.Length > 0)) // オーディオソースとマイクがある
		{
			string devName = Microphone.devices[0]; // 複数見つかってもとりあえず0番目のマイクを使用
			int minFreq, maxFreq;
			Microphone.GetDeviceCaps (devName, out minFreq, out maxFreq); // 最大最小サンプリング数を得る
			aud.clip = Microphone.Start (devName, true, 2, minFreq); // 音の大きさを取るだけなので最小サンプリングで十分
			aud.Play (); //マイクをオーディオソースとして実行(Play)開始
		}
		//Enemy読み込み
		nowenemynum = enemynum;
	}

	// Update is called once per frame
	void Update () {
		if (!isPose () && !isDead) {
			if (!isClear) {
				//経過時間
				alltime += Time.deltaTime;
				float printtime = limittime - alltime;
				if (printtime < 0) {
					printtime = 0;
					isDead = true;
				}
				timeText.text = ((int) printtime).ToString ();
				//スコア表示
				scoreText.text = score.ToString ();
				charaplace.transform.rotation = Quaternion.Euler (0,
					camera.transform.localEulerAngles.y,
					0);

				//Voice
				int volume = (int) (m_volumeRate * 100);
				test.text = volume.ToString ();
				VoicebarUpdate (m_volumeRate * 100);

				//画面中心
				Vector3 center = new Vector3 (Screen.width / 2, Screen.height / 2);
				//カメラから画面中心へのray
				Ray ray = Camera.main.ScreenPointToRay (center);
				//Ray ray = new Ray (transform.position, new Vector3 (0, 0.2f, 1.0f));

				RaycastHit hit;
				int distance = 10;
				//可視化
				Debug.DrawLine (ray.origin, ray.direction * distance, Color.red);
				//Rayが衝突
				if (Physics.Raycast (ray, out hit, distance)) {
					lockEnemys.Clear ();
					foreach (RaycastHit hits in Physics.RaycastAll (ray)) {
						if (hits.collider.tag == "Enemy") {
							lockon = true;
							lockEnemys.Add (hits.collider.gameObject);
							//Debug.Log ("RayがEnemyに当たった");
							//Debug.Log(hits.collider.gameObject.transform.position);
							//Destroy(hits.collider.gameObject);
						}
					}
				} else {
					lockon = false;
				}

				if (lockon && volume >= 10) {
					attackonEnemy (bigAT);
				} else if (lockon && Input.GetMouseButtonUp (0)) {
					attackonEnemy (smallAT);
				}
				//Enemy
				if (nowenemynum > 0) {
					enemyappercount += Time.deltaTime;
					//３秒枚
					if (enemyappercount > 3) {
						//enemy生成
						loadEnemy (nowenemynum);
						nowenemynum--;
						enemyappercount -= 3;
					}
				}
			} else {
				//Clear処理
				if (clearResult) {
					resultCanvas.SetActive (true);
					var effect = Resources.Load ("effect/ClearEffect") as GameObject;
					GameObject effects = GameObject.Instantiate (effect,
						new Vector3 (10 * Mathf.Sin (camera.transform.localEulerAngles.y * (Mathf.PI / 180)),
							2.5f,
							10 * Mathf.Cos (camera.transform.localEulerAngles.y * (Mathf.PI / 180))),
						Quaternion.identity);
					effects.transform.Rotate (0, camera.transform.localEulerAngles.y, 0);
					clearResult = false;
					Invoke ("printTap", 1);
				}
				if (isScore) {
					//スコア画面表示
				}
			}
		} else {
			//Pose中
			if (isDead) {
				//死亡処理
			}
		}
	}

	private void loadCharacter () {
		var homechara = "cecil_sharo"; //UserDataLoader.userdata.character;//"cecil_sharo";
		var chara = Resources.Load ("chara/" + homechara) as GameObject;
		character = GameObject.Instantiate (chara, new Vector3 (0, 0, 3), Quaternion.identity) as GameObject;
		//character.transform.Rotate(new Vector3(0,0,0));
		character.transform.parent = charaplace.transform;
	}

	// オーディオが読まれるたびに実行される
	private void OnAudioFilterRead (float[] data, int channels) {
		float sum = 0f;
		for (int i = 0; i < data.Length; ++i) {
			sum += Mathf.Abs (data[i]); // データ（波形）の絶対値を足す
		}
		// データ数で割ったものに倍率をかけて音量とする
		m_volumeRate = Mathf.Clamp01 (sum * m_gain / (float) data.Length);
	}
	//敵出現
	private void loadEnemy (int nowe) {
		//int place = Random.Range (0, 10);
		float theta = Random.Range (0, 360);
		int number = enemynum - nowe;
		Enemydata data = enemys[enemydata[number]];
		GameObject enemy = Resources.Load ("enemy/" + data.modelname) as GameObject;
		//flag,HP,AT
		enemy.GetComponent<enemyscript> ().Setdata (
			data.typeflag, data.enemyHP, data.enemyAT);
		var newenemy = GameObject.Instantiate (enemy,
			new Vector3 (10 * Mathf.Sin (theta * (Mathf.PI / 180)), 1, 10 * Mathf.Cos (theta * (Mathf.PI / 180))), Quaternion.identity) as GameObject;
		newenemy.transform.Rotate (0, theta, 0);

	}

	//敵へ攻撃
	private void attackonEnemy (int AT) {
		foreach (var enemy in lockEnemys) {
			bool isBreak = enemy.GetComponent<enemyscript> ().Damage (AT);
			if (isBreak) {
				Destroy (enemy);
				//撃破数等加算
				breakenemynum++;
				if (enemynum == breakenemynum) {
					isClear = true;
				}
			}
		}
		lockEnemys.Clear ();
	}

	private void VoicebarUpdate (float volume) {
		float amount = (float) volume / 25.0f;
		volumeBar.GetComponent<Image> ().fillAmount = amount;
	}

	//ホームシーン遷移
	public void BackHome () {
		SceneManager.LoadScene ("Home");
	}
	//Pose判定
	public bool isPose () {
		return poseMenu.activeInHierarchy;
	}
	//敵衝突
	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.tag == "Enemy") {
			Debug.Log ("enemy at");
			//ダメージ
			int damage = collider.gameObject.GetComponent<enemyscript> ().enemyAT;
			charaHP -= damage;
			if (charaHP <= 0) isDead = true;
			HPbarUpdate ();
		}
	}
	void HPbarUpdate () {
		float nowhp = (float) charaHP / charaHPMax;
		hpBar.GetComponent<Image> ().fillAmount = nowhp;
	}
	bool checkDead () {
		if (charaHP <= 0) return true;
		if (limittime <= 0) return true;
		return false;
	}
	void printTap () {
		tapText.SetActive (true);
		isScore = true;
	}
}
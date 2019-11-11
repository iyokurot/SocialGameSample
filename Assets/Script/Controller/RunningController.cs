using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RunningController : MonoBehaviour {
	[SerializeField]
	private GameObject camera;
	public GameObject[] stages;
	private float scoretime = 0;
	private float gametime = 0;
	private float starttime = 4.0f;
	public Text startcount;
	private bool isStart = true;
	private int accelable = 0;
	private int camerarotateY = 0;

	private Animator animater;
	private GameObject character;
	private CharacterController charaController;
	private float speed = 10.0f;
	private int MaxHP = 3;
	private int HP;
	private int coin = 0;
	public Text coincount;
	public GameObject HPbar;

	private List<GameObject> generateStages = new List<GameObject> ();
	private int stageindex = 0;

	private Vector3 touchStartPosition;
	private Vector3 touchEndPosition;

	private bool jumping = false;
	private float theta = 0;
	private Vector3 direction = Vector3.zero;

	public Text gameover;
	public GameObject resultmenu;
	public Text cointext;
	public Text timetext;
	public Text misstext;
	public Text scoretext;
	private float resultscore = 0;

	private bool datawrite = true;
	[SerializeField]
	private CharacterDataBase characterDataBase;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 3; i++) {
			GenerateStage (0);
		}
		loadCharacter ();
		animater = character.GetComponent<Animator> ();
		charaController = character.GetComponent<CharacterController> ();
		animater.SetBool ("Runing", true);
		HP = MaxHP;
	}

	// Update is called once per frame
	void Update () {
		HPbarUpdate ();
		gametime += Time.deltaTime;

		if (isStart) {
			starttime -= Time.deltaTime;
			int timetext = (int) starttime;
			startcount.text = timetext.ToString ();
			if (starttime < 0) {
				startcount.gameObject.SetActive (false);
				isStart = false;
			}
		}
		StageCheck ();
		//生きている
		if (HP > 0) {
			scoretime += Time.deltaTime;
			Running ();
			if (Input.GetMouseButton (0)) {
				MoveSide (Input.mousePosition.x);
			}

			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				touchStartPosition = new Vector3 (Input.mousePosition.x,
					Input.mousePosition.y,
					Input.mousePosition.z);
			}
			if (Input.GetKeyUp (KeyCode.Mouse0)) {
				touchEndPosition = new Vector3 (Input.mousePosition.x,
					Input.mousePosition.y,
					Input.mousePosition.z);
				GetDirection ();
			}
			MoveJump ();
			//加速判定
			Accelerater ();
			//カメラ回転判定
			CameraRotation ();
			//コインＵＩ
			CoinCountUpdate ();
		} else {
			//ゲームオーバー処理
			gameover.gameObject.SetActive (true);
			Invoke ("GameOverMethod", 2);

		}
	}

	//キャラロード
	private void loadCharacter () {
		CharacterData homechara = GetCharacter (UserDataLoader.userdata.character);
		character = GameObject.Instantiate (homechara.GetModel ()) as GameObject;
		character.transform.Rotate (new Vector3 (0, 180, 0));
	}
	private CharacterData GetCharacter (int id) {
		return characterDataBase.GetList ().Find (chara => chara.GetId () == id);
	}

	//ステージ生成
	private void GenerateStage (int n) {
		//ステージランダム生成
		int stagenum;
		if (n == 0) {
			stagenum = 0;
			} else {
			stagenum = Random.Range (1, stages.Length);
		}

		GameObject stage = GameObject.Instantiate (
			stages[stagenum],
			new Vector3 (0, 0, stageindex),
			Quaternion.identity
		) as GameObject;
		generateStages.Add (stage);
		stageindex += 20;
	}
	//ステージ更新チェック
	private void StageCheck () {
		float camerapos = camera.transform.position.z;
		float len = stageindex - camerapos;
		if (120 > len) {
		GenerateStage (1);

		}
		int stagelength = generateStages.Count;
		if (stagelength > 8) {
			GameObject oldstage = generateStages[0];
			generateStages.RemoveAt (0);
			Destroy (oldstage);
		}
	}

	private void Running () {
		//キャラ落下
		if (character.transform.position.y < -10) {
			HP = 0;
		}
		//キャラ移動
		float moveZ = speed;
		Vector3 direction = new Vector3 (0, 0, moveZ);
		charaController.SimpleMove (direction);

		//追尾カメラ移動
		//camera.transform.position+=camera.transform.forward*speed*Time.deltaTime;
		camera.transform.position = new Vector3 (
			0, //character.transform.position.x,
			0, //character.transform.position.y,
			character.transform.position.z + 8
		);
	}
	//キャラ横移動
	private void MoveSide (float pos) {
		float center = Screen.width / 2;
		Vector3 direction;
		if (center > pos) {
			direction = new Vector3 (-2, 0, 0);
			charaController.SimpleMove (direction);
		} else if (center < pos) {
			direction = new Vector3 (2, 0, 0);
			charaController.SimpleMove (direction);
		}
	}

	//キャラジャンプ
	private void MoveJump () {
		if (jumping && charaController.isGrounded) {
			animater.SetTrigger ("JumpingTrigger");
			direction.y = 10;
		}
		jumping = false;
		direction.y -= 10 * Time.deltaTime;
		charaController.Move (direction * Time.deltaTime);
	}

	//スライド方向
	private void GetDirection () {
		float directionX = touchEndPosition.x - touchStartPosition.x;
		string Direction;
		if (30 < directionX) {
			//右向き
			Direction = "right";
		} else if (-30 > directionX) {
			//左向き
			Direction = "left";
		} else {
			//タッチ
			Direction = "touch";
		}

		switch (Direction) {

			case "right":
				break;

			case "left":
				break;

			case "touch":
				jumping = true;
				break;

			default:
				break;
		}
	}
	//加速チェック
	private void Accelerater () {
		if (coin != accelable && coin % 3 == 0) {
			accelable = coin;
			speed *= 1.1f;
		}
	}
	//カメラ回転
	private void CameraRotation () {
		if (gametime > 10) {
			gametime = 0;
			int rad = 0;
			switch (camerarotateY) {
				case 0:
					camerarotateY = 15;
					rad = 15;
					break;
				case 15:
					camerarotateY = -15;
					rad = -30;
					break;
				case -15:
					camerarotateY = 0;
					rad = 15;
					break;
			}
			camera.transform.Rotate (new Vector3 (0, rad, 0));
		}
	}
	private void CoinCountUpdate () {
		coincount.text = "coin:" + coin.ToString ();
	}

	public void CharaCollision () {
		Debug.Log ("hit!");
	}
	//敵と衝突
	public void EnemyCollision () {
		//後退
		Vector3 pos = Vector3.zero;
		pos.z = -3;
		charaController.Move (pos);
		//ダメージ
		HP--;
		//Debug.Log("HP"+HP);
	}
	//コイン獲得
	public void CoinCollision () {
		//
		coin++;
		//Debug.Log("coin"+coin);
	}
	private void HPbarUpdate () {
		float amount = (float) HP / MaxHP;
		HPbar.GetComponent<Image> ().fillAmount = amount;
	}

	private void GameOverMethod () {
		resultmenu.SetActive (true);
		int random = Random.Range (1000, 9999);
		string rmd = random.ToString ();
		cointext.text = rmd;
		timetext.text = rmd;
		misstext.text = rmd;
		scoretext.text = rmd;
		resultscore += Time.deltaTime;
		if (resultscore > 1) {
			cointext.text = coin.ToString ();
		}
		if (resultscore > 1.5) {
			timetext.text = scoretime.ToString ();
		}
		if (resultscore > 2) {
			misstext.text = "0";
		}
		if (resultscore > 2.5) {

			scoretext.text = calcscore ().ToString ();
			if (Input.GetMouseButton (0)) {
				returnScene ();
			}
		}
		if (datawrite) {
			UserDataLoader.userdata.coin += coin;
			datawrite = false;
		}
	}

	private int calcscore () {
		int score = 0;
		score += coin * 10;
		score += (int) scoretime;
		score -= 0; //miss
		return score;
	}

	//シーンバック
	private void returnScene () {
		SceneManager.LoadScene ("Home");
	}
}
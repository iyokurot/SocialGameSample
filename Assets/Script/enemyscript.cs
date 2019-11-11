using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour {
	float time = 0;
	float movespeed = 0.03f; //ムーブ速度
	float movingdistance = 0f;
	public string typeflag; //属性
	public int enemyHP; //敵体力
	public int enemyAT; //敵攻撃
	[SerializeField]
	private GameObject effect;
	private float effectdeleteTime = 1.0f;

	// Use this for initialization
	void Start () { }

	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 3) {
			if (typeflag == "Normal") {
				this.MoveTypePhisics ();
			} else if (typeflag == "Magic") {
				this.MoveTypeMagic ();
			} else {

			}
		}
	}
	//データ設定
	public void Setdata (string flag, int hp, int at) {
		//敵データ挿入
		typeflag = flag;
		enemyHP = hp;
		enemyAT = at;
	}
	//物理敵
	void MoveTypePhisics () {
		//突進のち10離れた位置にて５秒後に再度突進
		this.transform.Translate (0, 0, -movespeed);
		movingdistance += movespeed;
		if (movingdistance >= 20) {
			time = -2.0f;
			movingdistance = 20.0f - movingdistance;
			this.transform.Rotate (0, 180, 0);
		}
	}
	//魔法敵
	void MoveTypeMagic () {
		//
		//Debug.Log("type magic");
	}
	public bool Damage (int damage) {
		//エフェクト
		var damageeffect = GameObject.Instantiate (effect, transform.position, Quaternion.identity) as GameObject;
		Destroy (damageeffect, effectdeleteTime);
		//ダメージ計算＆死亡判定
		enemyHP -= damage;
		if (enemyHP <= 0) {
			return true;
		} else {
			return false;
		}
	}
	void OnTriggerEnter (Collider collider) {
		//Debug.Log(collider);		
	}
	void OnCollisionEnter (Collision collision) {
		//Debug.Log("col");		
	}
}
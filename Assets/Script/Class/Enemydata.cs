using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵DB用データクラス
[Serializable]
[CreateAssetMenu (fileName = "enemy", menuName = "CreateEnemy")]
class Enemydata : ScriptableObject {
	public enum EnemyType {
		Normal,
		Magic
	}

	[SerializeField]
	private string modelname; //読み込みモデル名
	[SerializeField]
	private GameObject model;
	[SerializeField]
	private EnemyType type; //属性
	[SerializeField]
	private int enemyHP; //敵体力
	[SerializeField]
	private int enemyAT; //敵攻撃
	public string GetModelname () {
		return modelname;
	}
	public GameObject GetModel () {
		return model;
	}
	public EnemyType GetType () {
		return type;
	}
	public int GetEnemyHP () {
		return enemyHP;
	}
	public int GetEnemyAT () {
		return enemyAT;
	}
}
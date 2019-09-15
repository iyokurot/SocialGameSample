using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Enemydata {
	public string modelname; //読み込みモデル名
	public int typeflag; //物理or魔法
	public int enemyHP; //敵体力
	public int enemyAT; //敵攻撃
	public Enemydata (string m, int t, int hp, int at) {
		modelname = m;
		typeflag = t;
		enemyHP = hp;
		enemyAT = at;
	}
}
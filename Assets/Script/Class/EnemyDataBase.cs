using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "enemydb", menuName = "CreateEnemyDB")]
class EnemyDataBase : ScriptableObject {
	[SerializeField]
	private List<Enemydata> enemylists = new List<Enemydata> ();

	public List<Enemydata> GetList () {
		return enemylists;
	}
}
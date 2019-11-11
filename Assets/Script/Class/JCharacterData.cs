using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JCharacterData {
	public int id; //キャラID
	public int level; //レベル
	public int skillLevel;

	public JCharacterData (int id, int level, int skill) {
		this.id = id;
		this.level = level;
		this.skillLevel = skill;
	}
	public JCharacterData () { }
}
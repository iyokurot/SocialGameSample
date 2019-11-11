using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "characterdb", menuName = "CreateCharacterDB")]
class CharacterDataBase : ScriptableObject {
	[SerializeField]
	private List<CharacterData> list = new List<CharacterData> ();
	public List<CharacterData> GetList () {
		return list;
	}
}
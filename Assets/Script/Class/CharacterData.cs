using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
[CreateAssetMenu (fileName = "character", menuName = "CreateCharacter")]
class CharacterData : ScriptableObject {
    [SerializeField]
    private int id; //PK、判別ID
    [SerializeField]
    private string modelname; //読み込みモデル名
    [SerializeField]
    private GameObject model; //読み込みモデル
    [SerializeField]
    private Sprite miniSprite; //小画像
    [SerializeField]
    private Sprite bigSprite; //大画像
    [SerializeField]
    private int HP; //体力
    [SerializeField]
    private int smallAT; //小攻撃
    [SerializeField]
    private int bigAT; //大攻撃
    [SerializeField]
    private string subTitle; //カードタイトル
    [SerializeField]
    private string charaName; //キャラ名
    [SerializeField]
    private string skillName; //スキル名
    [SerializeField]
    private string skillEffect; //スキル効果
    [SerializeField]
    private Sprite skillImage; //スキル画像
    [SerializeField, Range (0, 2)]
    private int rarelity; //レア度
    public int GetId () {
        return id;
    }
    public string GetModelname () {
        return modelname;
    }
    public GameObject GetModel () {
        return model;
    }
    public Sprite GetMiniSprite () {
        return miniSprite;
    }
    public Sprite GetBigSprite () {
        return bigSprite;
    }
    public int GetHP () {
        return HP;
    }
    public int GetSmallAT () {
        return smallAT;
    }
    public int GetBigAT () {
        return bigAT;
    }
    public string GetSubTitle () {
        return subTitle;
    }
    public string GetCharaName () {
        return charaName;
    }
    public string GetSkillName () {
        return skillName;
    }
    public string GetSkillEffect () {
        return skillEffect;
    }
    public Sprite GetSkillImage () {
        return skillImage;
    }
    public int GetRarelity () {
        return rarelity;
    }
}
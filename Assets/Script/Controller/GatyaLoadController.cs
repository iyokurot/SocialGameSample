using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//キャラガチャ実装
public class GatyaLoadController : MonoBehaviour {
    JsonController jsoncontroller = new JsonController ();
    private int drowCount = 1; //ガチャ回数
    private int printIndex = 0;
    [SerializeField]
    Text tap; //初回タップ文字
    [SerializeField]
    GameObject resultcanvas; //結果画面
    [SerializeField]
    GameObject content; //リザルト画面コンテント
    [SerializeField]
    GameObject charaPanel; //キャラ画像Prefab

    private float textspeed = 0.5f;
    private bool tapable = true;
    private float time = 0;

    private List<JCharacterData> charalist = new List<JCharacterData> (); //取得済みキャラリスト
    [SerializeField]
    CharacterDataBase characterDataBase; //キャラクターDB
    CharacterData character; //選出キャラ
    List<CharacterData> drowcharaList = new List<CharacterData> (); //選出キャラリスト
    GameObject characterObject; //キャラ保管Object
    [SerializeField]
    GameObject charaImage; //選出キャラ画像UI

    [SerializeField]
    GameObject magicEffect; //魔法陣周りのエフェクト（ノーマル
    [SerializeField]
    GameObject convergeEffect; //出現エフェクト
    GameObject coneffect; //エフェクト保管
    [SerializeField]
    GameObject appearEffect; //出現エフェクト拡散
    bool firstTap = true; //初回タップ
    bool isloadChangeEffect = false; //エフェクト変換（収束＝＞出現
    bool printImageAble = false; //画像表示
    bool resultAble = false; //結果画面

    void Start () {
        //取得済みキャラクター読み込み
        charalist = readJson ();
        System.Random rmd = new System.Random ();
        for (int i = 0; i < drowCount; i++) {
            //排出キャラ選出
            character = selectCharacter (rmd);
            drowcharaList.Add (character);
            //取得済みか確認、分岐処理
            createContent ();
        }
        //魔法陣パーティクル読み込み
        GameObject.Instantiate (magicEffect);
        //キャラ保存
        writeJson ();
    }

    void Update () {
        //初回タップ後エフェクト起動
        //光エフェクト＋３sかけてモデル表示
        //モデル出現後★エフェクトはじける（仮）
        //1s後画像表示
        //画像表示のちタップにてリザルト表示

        if (tapable) {
            //タップ文字点滅
            updateText ();
        }

        if (drowCount > 0) {
            if (Input.GetMouseButton (0)) {
                if (firstTap) {
                    //結果画像
                    charaImage.GetComponent<Image> ().sprite = drowcharaList[printIndex].GetBigSprite ();
                    //テキスト非表示
                    tap.gameObject.SetActive (false);
                    tapable = false;

                    coneffect = GameObject.Instantiate (convergeEffect);
                    StartCoroutine (derayMethod (5.0f, () => {
                        changeEffects (coneffect);
                    }));
                    StartCoroutine (derayMethod (1.0f, () => {
                        isloadChangeEffect = true;
                    }));
                    firstTap = false;
                }
                if (!firstTap && isloadChangeEffect) {
                    //タップでのスキップ
                    changeEffects (coneffect);
                }
                if (printImageAble) {
                    //キャラ画像表示（タップでのスキップ
                    appearImage ();
                }
                if (resultAble) {
                    //初期化
                    firstTap = true;
                    isloadChangeEffect = false;
                    printImageAble = false;
                    resultAble = false;
                    charaImage.SetActive (false);
                    Destroy (characterObject);
                    drowCount--;
                    printIndex++;
                }
            }
        } else {
            //結果画面
            resultcanvas.SetActive (true);
        }
    }
    //キャラ選出
    private CharacterData selectCharacter (System.Random rmd) {
        //レア度選出
        int rarelity = rmd.Next (3);
        //レア度別キャラ選出
        List<CharacterData> rarelist = GetCharaListByRarelity (rarelity);
        int charaindex = rmd.Next (rarelist.Count);
        return rarelist[charaindex];
    }
    //指定レアのキャラリスト
    private List<CharacterData> GetCharaListByRarelity (int rarelity) {
        List<CharacterData> resList = new List<CharacterData> ();
        foreach (var chara in characterDataBase.GetList ()) {
            if (rarelity == chara.GetRarelity ()) {
                resList.Add (chara);
            }
        }
        return resList;
    }
    //コンテント作成
    private void createContent () {
        //取得済み判定
        if (isGetCharacter (character.GetId ())) {
            //重複アイテム
        } else {
            //新規キャラ
            JCharacterData newChara = new JCharacterData (character.GetId (), 1, 1);
            charalist.Add (newChara);
        }
        charaPanel.GetComponent<Image> ().sprite = character.GetMiniSprite ();
        charaPanel.GetComponent<ContentCharaData> ().SetData (character.GetId ());
        GameObject panel = Instantiate (charaPanel, content.transform);
    }
    //IDから取得済み判定
    private bool isGetCharacter (int charaId) {
        foreach (var chara in charalist) {
            if (charaId == chara.id) {
                return true;
            }
        }
        return false;
    }

    //取得キャラ書き込み
    private void writeJson () {
        string jsonstr = JsonUtility.ToJson (new Serialize<JCharacterData> (charalist)); //JsonUtility.ToJson (getitems);
        jsoncontroller.writeJson (jsonstr, "/characterdata.json");

    }
    //取得キャラ読み込み
    private List<JCharacterData> readJson () {
        string datastr = jsoncontroller.readJsonChangeable ("/characterdata.json");
        Debug.Log (datastr);
        return JsonUtility.FromJson<Serialize<JCharacterData>> (datastr).ToList ();
    }

    //モデルデータ一覧読み込み
    private List<Itemdata> readmodels () {
        string datastr = jsoncontroller.readJson ("data/modeldata");
        return JsonUtility.FromJson<Serialize<Itemdata>> (datastr).ToList ();
    }

    private void updateText () {
        var color = tap.color;
        time += Time.deltaTime * 5.0f * textspeed;
        color.a = Mathf.Sin (time) * 0.5f + 0.5f;
        tap.color = color;
    }

    private IEnumerator derayMethod (float time, Action action) {
        yield return new WaitForSeconds (time);
        action ();
    }

    private void changeEffects (GameObject effect) {
        if (isloadChangeEffect) {
            //収束エフェクト削除
            Destroy (effect);
            //出現エフェクト表示＆モデル表示
            characterObject = GameObject.Instantiate (drowcharaList[printIndex].GetModel (), new Vector3 (0, 0, 0), Quaternion.identity);
            characterObject.transform.Rotate (0, 180, 0);
            GameObject aEffect = GameObject.Instantiate (appearEffect);

            StartCoroutine (derayMethod (2.0f, () => {
                appearImage ();
                Destroy (aEffect);
            }));
            StartCoroutine (derayMethod (1.0f, () => {
                printImageAble = true;
            }));
            isloadChangeEffect = false;
        }
    }
    //排出画像表示
    private void appearImage () {
        if (printImageAble) {
            //画像表示
            charaImage.SetActive (true);
            printImageAble = false;
            StartCoroutine (derayMethod (1.0f, () => {
                resultAble = true;
            }));
        }

    }

    public void onClickBackInresult () {
        //ガチャトップ画面ロード
        SceneManager.LoadScene ("GatyaTop");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TopController : MonoBehaviour
{
    public GameObject question;
    public Text jewel;
    [SerializeField]
    private int gatya;
    private int afterjewel;
    // Start is called before the first frame update
    void Start()
    {
        jewelTextUploader();
    }

    // Update is called once per frame
    void Update()
    {
        if(UserDataLoader.userdata.jewel>(afterjewel+gatya)){
            jewelTextUploader();
        }
    }

    private void jewelTextUploader(){
        afterjewel=UserDataLoader.userdata.jewel-gatya;
        jewel.text="石"+UserDataLoader.userdata.jewel+" → "+afterjewel;
        if(afterjewel<0){
            jewel.text=jewel.text+"\n石が足りません";
        }
    }

    public void onClickgatya(){
        question.SetActive(true);
    }

    public void onClickBack(){
        SceneManager.LoadScene("Home");
    }

    public void onClickYes(){
        //石マイナス処理
        if(afterjewel<0){
            //石不足
            
        }else{
            UserDataLoader.userdata.jewel=afterjewel;
            //ガチャ画面ロード
            SceneManager.LoadScene("GatyaLoad");
        }

        
    }
    public void onClickNo(){
        question.SetActive(false);
    }
}

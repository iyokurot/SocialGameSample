using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GatyaLoadController : MonoBehaviour
{
    JsonController jsoncontroller=new JsonController();

    public Text tap;
    public Text cons;
    public GameObject resultcanvas;

    private float textspeed=0.5f;
    private bool tapable=true;
    private float time=0;
    private int getcount=0;

    private bool collisionsenser=false;
    private bool resultbool=true;
    private GameObject drawBall;

    
    private List<Itemdata> modellist=new List<Itemdata>();
    private List<GetItems> itemlist=new List<GetItems>();
    // Start is called before the first frame update
    void Start()
    {
        modellist=readmodels();        
        itemlist=readJson();
    }

    // Update is called once per frame
    void Update()
    {
        if(tapable){
            updateText();
        }

        if(Input.GetMouseButton(0)){
            //テキスト非表示
            tap.gameObject.SetActive(false);
            tapable=false;
            //ボール読み込み
            while(getcount<1){
                //ランダム選択
                getcount++;
                System.Random rmd=new System.Random();
                int ballrare=rmd.Next(3);
                Itemdata selectball=createDrawBall(ballrare+1);
                writeJson(selectball);
            }

            if(collisionsenser&&resultbool){
                collisionsenser=false;
                resultbool=false;
                resultcanvas.SetActive(true);
                Vector3 pos=this.transform.position;
                pos.y=pos.y+10;
                var ball= GameObject.Instantiate(drawBall,pos, this.transform.rotation) as GameObject;
                ball.GetComponent<Rigidbody>().useGravity=false;
                
            }

        }
    }

    //ボール生成
    private Itemdata createDrawBall(int i){
        Vector3 pos=this.transform.position;
        pos.y=pos.y+10;
        //レア度のオブジェクト取得ーリスト作成
        List<Itemdata> list=new List<Itemdata>();
        foreach (var model in modellist)
        {
            if(i==model.rarelity){
                list.Add(model);
            }
        }
        //レア度別リストからランダム取得
        System.Random rmd=new System.Random();
        int listlength=list.Count;
        int modelno=rmd.Next(listlength);
        Itemdata selectmodel=list[modelno];

        string modelname=list[0].modelname;
        drawBall=Resources.Load(modelname) as GameObject;
        var selectedball = GameObject.Instantiate(drawBall,pos, this.transform.rotation) as GameObject;
        return selectmodel;
    }

    //取得アイテムに書き込み
    private void writeJson(Itemdata selectball){
        GetItems getitems=new GetItems();
        getitems.itemid=selectball.itemid;
        getitems.date=2019;
        getitems.rarelity=selectball.rarelity;

        itemlist.Add(getitems);

        string jsonstr = JsonUtility.ToJson(new Serialize<GetItems>(itemlist));//JsonUtility.ToJson (getitems);
        jsoncontroller.writeJson(jsonstr,"/savedata.json");
        
    }

    //取得アイテム読み込み
    private List<GetItems> readJson(){
        string datastr = jsoncontroller.readJsonChangeable("/savedata.json");
        //cons.text=datastr;
        return JsonUtility.FromJson<Serialize<GetItems>>(datastr).ToList();
    }

    //モデルデータ一覧読み込み
    private List<Itemdata> readmodels(){
        string datastr = jsoncontroller.readJson("data/modeldata");
        return JsonUtility.FromJson<Serialize<Itemdata>>(datastr).ToList();
    }

    private void updateText(){
        var color=tap.color;
		time+=Time.deltaTime*5.0f*textspeed;
		color.a=Mathf.Sin(time)*0.5f+0.5f;
		tap.color=color;
    }

    //グラウンドがCollision
    public void collisionCall(){
        collisionsenser=true;
    }

    public void onClickBackInresult(){
        //ガチャトップ画面ロード
        SceneManager.LoadScene("GatyaTop");
    }
}

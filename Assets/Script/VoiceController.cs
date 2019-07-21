using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VoiceController : MonoBehaviour {
	
	[SerializeField]
	private GameObject camera;

	private Animator animater;
	private GameObject character;
	private CharacterController charaController;
	public GameObject charaplace;

	public Text test;

	[SerializeField, Range(0f, 10f)] float m_gain = 1f; // 音量に掛ける倍率
    float m_volumeRate; // 音量(0-1)

	// Use this for initialization
	void Start () {
		loadCharacter();
		animater=character.GetComponent<Animator>();
		charaController=character.GetComponent<CharacterController>();
		animater.SetBool("Stay",true);

		AudioSource aud = GetComponent<AudioSource>();
        if ((aud != null)&&(Microphone.devices.Length>0)) // オーディオソースとマイクがある
        {
            string devName = Microphone.devices[0]; // 複数見つかってもとりあえず0番目のマイクを使用
            int minFreq, maxFreq;
            Microphone.GetDeviceCaps(devName, out minFreq, out maxFreq); // 最大最小サンプリング数を得る
            aud.clip = Microphone.Start(devName, true, 2, minFreq); // 音の大きさを取るだけなので最小サンプリングで十分
            aud.Play(); //マイクをオーディオソースとして実行(Play)開始
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		charaplace.transform.rotation=Quaternion.Euler(0,
		camera.transform.localEulerAngles.y,
		0);
		Debug.Log(m_volumeRate);
		int volume=(int)(m_volumeRate*100);
		test.text=volume.ToString();
		
	}

	private void loadCharacter(){
		var homechara="cecil_sharo";//UserDataLoader.userdata.character;//"cecil_sharo";
		var chara=Resources.Load("chara/"+homechara) as GameObject;
		character=GameObject.Instantiate(chara,new Vector3(0,0,3),Quaternion.identity) as GameObject;
		//character.transform.Rotate(new Vector3(0,0,0));
		character.transform.parent=charaplace.transform;
	}

	// オーディオが読まれるたびに実行される
    private void OnAudioFilterRead(float[] data, int channels)
    {
        float sum = 0f;
        for (int i = 0; i < data.Length; ++i)
        {
            sum += Mathf.Abs(data[i]); // データ（波形）の絶対値を足す
        }
        // データ数で割ったものに倍率をかけて音量とする
        m_volumeRate = Mathf.Clamp01(sum * m_gain / (float)data.Length);
    }
}

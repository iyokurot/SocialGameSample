using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceCameraController : MonoBehaviour {

    //private GUIStyle labelStyle;
    Quaternion start_gyro;
    Quaternion gyro;

    private GameObject controller;
    void Start () {
        controller = GameObject.Find ("VoiceGameController");
        /*
        this.labelStyle = new GUIStyle();
        this.labelStyle.fontSize = Screen.height / 22;
        this.labelStyle.normal.textColor = Color.white;
        */
        //ジャイロ値取得
        　　　　　
        start_gyro = new Quaternion (0, 0, 0, 0); //StartCameraController.ini_gyro;

    }

    void Update () {
        if (!isPose ()) {
            Input.gyro.enabled = true;
            if (Input.gyro.enabled) {
                gyro = Input.gyro.attitude;
                gyro = Quaternion.Euler (90, 0, 0) * (new Quaternion (-gyro.x, -gyro.y, gyro.z, gyro.w));
                this.transform.localRotation = gyro;
                //最初に見ていた向きとゲームの進行方向を合わせる
                //this.transform.localRotation = Quaternion.Euler(0, -start_gyro.y, 0);
            }
        }
    }
    //ジャイロセンサの値を表示するプログラム
    /*
    void OnGUI () {
        if (Input.gyro.enabled) {
            float x = Screen.width / 10;
            float y = 0;
            float w = Screen.width * 8 / 10;
            float h = Screen.height / 20;

            for (int i = 0; i < 3; i++) {
                y = Screen.height / 10 + h * i;
                string text = string.Empty;

                switch (i) {
                    case 0: //X
                        text = string.Format ("gyro-X:{0}", gyro.x);
                        break;
                    case 1: //Y
                        text = string.Format ("gyro-Y:{0}", gyro.y);
                        break;
                    case 2: //Z
                        text = string.Format ("gyro-Z:{0}", gyro.z);
                        break;
                    default:
                        throw new System.InvalidOperationException ();
                }

                GUI.Label (new Rect (x, y, w, h), text, this.labelStyle);
            }
        }
    }
    */

    private bool isPose () {
        bool pose = controller.GetComponent<VoiceController> ().isPose ();
        return pose;
    }
}
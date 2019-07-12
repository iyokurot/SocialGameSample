using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class JsonController
{
    public string readJson(string path){
        string datastr = "";
        /* 
        StreamReader reader;
        reader = new StreamReader (Application.dataPath +"/Resources"+ path);
        datastr = reader.ReadToEnd ();
        reader.Close ();
        */

        TextAsset txt = Resources.Load(path) as TextAsset;
        datastr = txt.text;
        return datastr;
    }

    public string readJsonChangeable(string name){
        string datastr = ""; 
        if(File.Exists(Application.persistentDataPath +name)){
        StreamReader reader;
        reader = new StreamReader (Application.persistentDataPath +name);
        datastr = reader.ReadToEnd ();
        reader.Close ();
        }else{
            Debug.Log("nofile");
            File.Create(Application.persistentDataPath +name);
        }
        
        return datastr;
    }

    public void writeJson(string json,string name){
        StreamWriter writer;

        writer = new StreamWriter(Application.persistentDataPath +name, false);
        writer.Write (json);
        writer.Flush ();
        writer.Close ();   
    }
}

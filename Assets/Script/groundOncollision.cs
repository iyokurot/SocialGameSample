using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundOncollision : MonoBehaviour
{
    GameObject controller;
    void Start(){
        controller=GameObject.Find("Controller");
    }
    
    void OnCollisionEnter(Collision collision){
        controller.GetComponent<GatyaLoadController>().collisionCall();
    }
}

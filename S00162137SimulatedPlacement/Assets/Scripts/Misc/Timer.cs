using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {


    public float timeCounter;
    public float lastTimeChange;



	// Use this for initialization
	void Start () {
        timeCounter = 20f;
	}
	
	// Update is called once per frame
	void Update () {
        timeCounter -= Time.deltaTime;


	}
}

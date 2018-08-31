using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveManager : MonoBehaviour {

    public float MaxTimer = 300f;
    public float Timer;
    public List<GameObject> Gibblers = new List<GameObject>();
    public float hovelSpawnTimer;

    private Text timerText;
    


    //different waves
    public int waveCount;

    [Header("NPC")]
    public int NumberOfGrunts;
    public GameObject grunt;

    public int NumberOfBrutes;
    public GameObject brute;

    public int NumberOfMothers;
    public GameObject mother;

    public int NumberOfBehemoths;
    public GameObject behemoth;




    // Use this for initialization
    void Start () {
        Timer = MaxTimer;
        //first wave
        MakeWaves();
        timerText = GameObject.Find("/Canvas/WaveTimeImage/timertext").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        int tempTimer = (int)Timer;
        Timer -= Time.deltaTime;
        timerText.text = tempTimer.ToString();
        if (Timer <= 0)
        {
            Timer = MaxTimer;
            BeginWave();

        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            BeginWave();
        }

	}

    //Attacks
    public void BeginWave()
    {
        foreach (GameObject gibbler in Gibblers)
        {
            gibbler.GetComponent<Gibbler>().attacking = true;
        }


    }

    //Determine Wave
    public void MakeWaves()
    {
        Gibblers.Clear();
        switch (waveCount)
        {
            //wave 1
            case 0:

                NumberOfBehemoths = 0;
                NumberOfMothers = 0;
                NumberOfBrutes = 0;
                NumberOfGrunts = 5;
                break;

            case 1:
                NumberOfBehemoths = 0;
                NumberOfMothers = 0;
                NumberOfBrutes = 1;
                NumberOfGrunts = 6;

                break;

             case 2:
                NumberOfBehemoths = 0;
                NumberOfMothers = 0;
                NumberOfBrutes = 0;
                NumberOfGrunts = 7;

                break;

            case 3:
                NumberOfBehemoths = 0;
                NumberOfMothers = 0;
                NumberOfBrutes = 2;
                NumberOfGrunts = 7;

                break;

                //wave5
            case 4:
                NumberOfBehemoths = 0;
                NumberOfMothers = 1;
                NumberOfBrutes = 2;
                NumberOfGrunts = 7;

                break;

                //wave6
            case 5:
                NumberOfBehemoths = 0;
                NumberOfMothers = 0;
                NumberOfBrutes = 2;
                NumberOfGrunts = 8;

                break;

                //wave 7
            case 6:
                NumberOfBehemoths = 0;
                NumberOfMothers = 1;
                NumberOfBrutes = 2;
                NumberOfGrunts = 8;

                break;

            case 7:
                NumberOfBehemoths = 0;
                NumberOfMothers = 1;
                NumberOfBrutes = 3;
                NumberOfGrunts = 9;

                break;

            case 8:
                NumberOfBehemoths = 0;
                NumberOfMothers = 1;
                NumberOfBrutes = 4;
                NumberOfGrunts = 9;

                break;
      


            default:

                NumberOfBehemoths = waveCount / 3;
                NumberOfMothers = waveCount / 3;
                NumberOfBrutes = waveCount / 3;
                NumberOfGrunts =  waveCount / 3;

                break;



        }//end switch


        //Grunts
        for (int i = 0; i < NumberOfBehemoths; i++)
        {
            Gibblers.Add(behemoth);
        }
        //mothers
        for (int i = 0; i < NumberOfMothers; i++)
        {
            Gibblers.Add(mother);
        }
        //brutes    
        for (int i = 0; i < NumberOfBrutes; i++)
        {
            Gibblers.Add(brute);
        }
        for (int i = 0; i < NumberOfGrunts; i++)
        {
            Gibblers.Add(grunt);
        }


        waveCount++;


    }


}

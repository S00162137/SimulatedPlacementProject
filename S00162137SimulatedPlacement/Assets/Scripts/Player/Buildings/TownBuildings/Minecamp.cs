using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecamp : Building {

    [Header("Mine Specific Variables")]
    //resouceCap increase
    public int StoneCapIncrease;
    public int MetalCapIncrease;

    //Resouce over timer
    public int StoneIncreaseAmount;
    public int MetalIncreaseAmount;


    //Timer Variables
    public float MetalTimer = 125f;
    private float mTimer;

    public float stoneTimer = 75f;
    private float Stimer;


    // Use this for initialization
    void Awake () {

        SPRenderer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        //SPRenderer = GetComponentInChildren<SpriteRenderer>();


        Stimer = 0;
        mTimer = 0;


        resources = GameObject.FindGameObjectWithTag(Tags.PLAYER_CONTROLLER_TAG).GetComponent<Resources>();
        resources.FoodCap += StoneCapIncrease;
        resources.MetalCap += MetalCapIncrease;
    }

    // Update is called once per frame
    void Update () {
        //Generate after Timer's up
        Stimer += Time.deltaTime;
        mTimer += Time.deltaTime;

        if (Stimer >= stoneTimer)
        {
            Stimer = 0;
            EndStoneTimer();
        }

        if (mTimer >= MetalTimer)
        {
            mTimer = 0;
            EndMetalTimer();
        }

    }


    void EndStoneTimer()
    {


        resources.Stone += StoneIncreaseAmount;
        if (resources.Stone > resources.StoneCap)
        {
            resources.Stone = resources.StoneCap;
        }
        resources.UpdateInfo();

        Stimer = 0;


    }

    void EndMetalTimer()
    {


        resources.Metal += MetalIncreaseAmount;
        if (resources.Metal > resources.MetalCap)
        {
            resources.Metal = resources.MetalCap;
        }
        resources.UpdateInfo();

        mTimer = 0;


    }


}

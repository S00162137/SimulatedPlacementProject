using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmHouse : Building {

    //For Upgrades


    //Resource Variables
    [SerializeField]
    private int FoodIncreaseAmount = 3;
    [SerializeField]
    private int foodCapIncreasesd = 20;


    //Timer Variables
    public float defaultTimer = 5f;
    private float timer;




    // Use this for initialization
    void Awake () {
        //Get Resources 
        //Set BuildingLevel
  

        SPRenderer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        //SPRenderer = GetComponentInChildren<SpriteRenderer>();
        timer = 0;
        resources = GameObject.FindGameObjectWithTag(Tags.PLAYER_CONTROLLER_TAG).GetComponent<Resources>();
        resources.FoodCap += foodCapIncreasesd;

    }





    // Update is called once per frame
    void Update () {

        //Generate after Timer's up
        timer += Time.deltaTime;
        if (timer >= defaultTimer)
        {
            timer = 0;
            EndTimer();
        }

    }


    void EndTimer()
    {


        resources.Food += FoodIncreaseAmount;
        if (resources.Food > resources.FoodCap)
        {
            resources.Food = resources.FoodCap;
        }
        resources.UpdateInfo();

        timer = 0;


    }

}

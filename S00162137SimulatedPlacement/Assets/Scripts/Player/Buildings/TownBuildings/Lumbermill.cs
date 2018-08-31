using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lumbermill : Building {

    //Sprites

    //Resource Variables

    [SerializeField]
    private int WoodIncreaseAmount = 2;
    [SerializeField]
    private int IncreaseWoodCapAmount = 20;

    //Timer Variables
    public float defaultTimer = 5f;
    private float timer;



    // Use this for initialization
    void Awake () {

        //Get Resources 
        //Set BuildingLevel

        resources = GameObject.FindGameObjectWithTag(Tags.PLAYER_CONTROLLER_TAG).GetComponent<Resources>();
        resources.WoodCap += IncreaseWoodCapAmount;

    }





    // Update is called once per frame
    void Update () {

        //Countdown to resource increase
        timer += Time.deltaTime;
        if (timer >= defaultTimer)
        {
            EndTimer();
        }

   

    }


    void EndTimer()
    {
        resources.Wood += WoodIncreaseAmount;
        if (resources.Wood > resources.WoodCap)
        {
            resources.Wood = resources.WoodCap;
        }
        resources.UpdateInfo();


        timer = 0;
        
    }
 
    public void Upgrade()
    {
        //Check if still Upgradable
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            //Set to sprite2
            if (currentLevel == 2)
            {
                SPRenderer.sprite = lvl2Sprite;

            }
            //Set to sprite3
            else
            {
                SPRenderer.sprite = lvl3Sprite;

            }
            AdjustValues();
        }



    }

    public void AdjustValues()
    {
        WoodIncreaseAmount += 3;
    }

 





}

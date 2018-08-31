using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResources : MonoBehaviour {

    //resources for the enemy player AI
    #region Stats Variables
    [Header("Starting Resources")]
    public int Food = 230;
    public int Wood = 50;
    public int Stone = 0;
    public int Metal = 0;
    public int People = 0; // total Pop

    public int Employed;
    public int Unemployed = 5;

    [Header("Starting Caps")]
    public int FoodCap = 100;
    public int WoodCap = 80;
    public int StoneCap = 50;
    public int MetalCap = 25;
    public int PeopleCap = 7;


    [Header("Resource Deduction")]
    public int peopleFoodCost;
    public int peopleWoodCost;

    #endregion

    EnemyBuildingManager buildManager;


    #region Threshhold Stats

    //Compared to generation costs, if lower than building needs to be done
    public int FoodGenAmount;
    public int WoodGenAmount;
    public int StoneGenAmount;


    public bool NeedToGetStones = false;

    [Header("ThreshHolds")]

    public int LowFoodCap;
    public int LowWoodCap;
    public int LowStoneCap;
    public int LowMetalCap;

    public int HighFoodCap;
    public int HighWoodCap;
    public int HighStoneCap;
    public int HighMetalCap;

    // If not enough villagers
    public int EnoughVillagersCap;
    public bool NeedVillagers = false;
    //If Too many villagers

    [Header("NeedResources")]
    public bool resourcesLow = false;
    public bool resourcesHigh = false;




    public bool lookForFood = false;
    public bool lookForWood = false;
    public bool lookForStone = false;
    public bool lookForMetal = false;

    #endregion







    private float timer = 0f;
    [SerializeField]
    private float defaultTimer = 20f;

    //ToDetermine If need to build
    public bool needHouses = false;
    public bool needFarms = false;
    public bool needBarracks = false;
    public bool needlumberMill = false;
    public bool needMine = false;
    public bool shouldBuild = false;

    // Use this for initialization
    void Start () {
        buildManager = GetComponent<EnemyBuildingManager>();
        DetermineLowThreshholds();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void CheckResourceNeeds()
    {

        resourcesLow = false;
        if (Food <= LowFoodCap)
        {
           resourcesLow = true;
           lookForFood = true;

         }
        else
        {
            lookForFood = false;
        }

        //wood
        if (Wood <= LowWoodCap)
        {
            resourcesLow = true;
            lookForWood = true;
        }
        else
        {
            lookForWood = false;
        }

        //stone
        if (Stone < StoneCap)
        {
            resourcesLow = true;
            lookForStone = true;
        }
        else
        {
            lookForStone = false;
        }



        if (Metal < MetalCap)
        {
            resourcesLow = true;
            lookForStone = true;

        }
        else
        {
            lookForStone = false;
        }



        CheckHighThreshHolds();


    }


    public void CheckHighThreshHolds()
    {
        if (Food >= HighFoodCap && Wood >= HighWoodCap && Stone >= HighStoneCap && Metal >= HighMetalCap)
        {
            resourcesHigh = true;
        }
        else
        {
            resourcesHigh = false;
        }

        CheckIfShouldBuild();
    }



    public void CheckIfShouldBuild()
    {



        if (resourcesHigh == true)
        {
            shouldBuild = true;
        }
        else
        {
            shouldBuild = false;
        }

        //Need Houses
        if (buildManager.NumberOfHouses < buildManager.RequiredHouses)
        {
            needHouses = true;
        }
        else
        {
            needHouses = false;
        }

        //Need Lumbermills
        if (buildManager.NumberOfLumber < buildManager.RequiredLumber)
        {
            needlumberMill = true;
        }
        else
        {
            needlumberMill = false;
        }

        //NeedFarms
        if (buildManager.NumberOFarms < buildManager.RequiredFarm)
        {
            needFarms = true;
        }
        else
        {
            needFarms = false;
        }

        //NeedMines
        if (buildManager.NumberOfMines < buildManager.RequiredMine)
        {
            needMine = true;
        }
        else
        {
            needMine = false;
        }


        //Need Barracks
        if (buildManager.NumberOfBarracks < buildManager.RequiredBarracks)
        {
            needBarracks = true;
        }
        else
        {
            needBarracks = false;
        }



    }


    //If Resources are lower than theses make more buildings
    public void DetermineLowThreshholds()
    {
        LowFoodCap = (FoodCap / 100) *20;
        LowWoodCap = (WoodCap / 100) * 20;
        LowStoneCap = (FoodCap / 100) * 20;
        LowMetalCap = (FoodCap / 100) * 20;

        HighFoodCap = 70;
        HighWoodCap = 70;
        HighStoneCap = 70;
        HighMetalCap = 70;


        if (Employed >= PeopleCap - 3)
        {
            needHouses = true;
        }


    }

}

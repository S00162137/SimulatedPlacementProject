using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuildingManager : MonoBehaviour {



    public GameObject LeftMostBuilding = null;
    public GameObject RightMostBuilding = null;


    #region building Prefabs
    [SerializeField]
    public GameObject HousePrefab;
    [SerializeField]
    public GameObject LumbermillPrefab;
    [SerializeField]
    public GameObject MinePrefab;
    [SerializeField]
    public GameObject FarmPrefab;
    [SerializeField]
    public GameObject BarracksPrefab;
    [SerializeField]
    public GameObject GatewayPrefab;
    [SerializeField]
    public GameObject TowerPrefab;


    public int NumberOfHouses;
    public int NumberOfLumber;
    public int NumberOFarms;
    public int NumberOfMines;
    public int NumberOfBarracks;
    public int NumberOfGateways;



    public float GameDuration;

    [Header("Required Building Numbers")]
    public int RequiredHouses = 2;
    public int RequiredLumber = 1;
    public int RequiredFarm = 1;
    public int RequiredMine = 0;
    public int RequiredBarracks = 1;
    public int RequiredGateWay = 1;


    public bool Timer5Called = false;
    public bool Timer10Called = false;
    public bool Timer15Called = false;


    #endregion

    //This script is used to manage buildings, determine if should build or not and if a buildings under attack

    //All buildings
    public List<GameObject> BuildingsList = new List<GameObject>();

    //Damaged Buildings
    public List<GameObject> DamagedBuildings = new List<GameObject>();


    public GameObject enemyTownhall;


    public bool NeedToDefend =false;
    public GameObject buildingUnderAttack;




    #region MiscBuildingLists
    //different building types
    public List<GameObject> EnemyFarms = new List<GameObject>();

    public List<GameObject> EnemyLumbermill = new List<GameObject>();

    public List<GameObject> EnemyHouse = new List<GameObject>();

    public List<GameObject> EnemyMine = new List<GameObject>();

    public List<GameObject> EnemyBarracks = new List<GameObject>();

    public List<GameObject> EnemyGateway = new List<GameObject>();

    public List<GameObject> EnemyTowers = new List<GameObject>();

    #endregion

    // Use this for initialization
    void Start () {
        enemyTownhall = GameObject.FindGameObjectWithTag(Tags.ENEMYBUILDING_TAG);
        RightMostBuilding = enemyTownhall;
	}
	
	// Update is called once per frame
	void Update () {

        GameDuration += Time.deltaTime;
        if ( 
            
            ((GameDuration >=  5*60) && (Timer5Called == false))
                                     ||
            ((GameDuration == 10 * 60) && (Timer10Called == false))
                                     ||
            ((GameDuration >= 15*60) && (Timer15Called == false))
           )
        {
            AssignNoRequired(GameDuration);
        }
    }

    public void AssignNoRequired(float timerValue)
    {
        if (timerValue < 10)
        {
            Timer5Called = true;

            RequiredHouses = 4;
            RequiredLumber = 2;
            RequiredMine = 1;
            RequiredFarm = 2;
            RequiredBarracks = 1;

        }
        else if (timerValue > 10 && timerValue <15)
        {
            Timer10Called = true;
            RequiredHouses = 6;
            RequiredLumber = 3;
            RequiredMine = 2;
            RequiredFarm = 3;
            RequiredBarracks = 2;

        }
        else if (timerValue > 15)
        {
            Timer15Called = true;
            RequiredHouses = 8;
            RequiredLumber = 4;
            RequiredMine = 3;
            RequiredFarm = 4;
            RequiredBarracks = 2;

        }
    }


    //Used for villager wandering
    public void FindTownLength()
    {
        foreach  (GameObject building in BuildingsList)
        {


        }


    }


    public void CheckIfUnderAttack()
    {
        //Check damaged buildings
        EnemyBuilding eBuilding = new EnemyBuilding();
        foreach (GameObject item in DamagedBuildings)
        {
            eBuilding = item.GetComponent<EnemyBuilding>();
            if (eBuilding.UnderAttack == true)
            {
                NeedToDefend = true;
            }
            else
            {
                NeedToDefend = false;
            }
        }
    }




}

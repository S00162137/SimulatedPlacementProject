using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Barracks : Building {



    //For Spawning Villagers
    private int spawnQ;
    public int maxSpawnQ;


    //The units in queue to be made
    private List<GameObject> BarracksQueue = new List<GameObject>();
    public GameObject nextUnit;

    #region units
    [Header("Unit Prefabs")]
    [SerializeField]
    private GameObject soldier;
    [SerializeField]
    private int soldierFoodCost;
    [SerializeField]
    private int soldierWoodCost;
    [SerializeField]
    private int soldierStoneCost;
    [SerializeField]
    private int soldierMetalCost;


    [SerializeField]
    private GameObject shieldBarer;
    [SerializeField]
    private int shieldFoodCost;
    [SerializeField]
    private int shieldWoodCost;
    [SerializeField]
    private int shieldStoneCost;
    [SerializeField]
    private int shieldMetalCost;


    [SerializeField]
    private GameObject pikeman;
    [SerializeField]
    private int pikeFoodCost;
    [SerializeField]
    private int pikeWoodCost;
    [SerializeField]
    private int pikeStoneCost;
    [SerializeField]
    private int pikeMetalCost;


#endregion


    [Header("QTimer")]
    public float defaultTimer = 20f;
   
    private float timer;
    public bool makeSolider = false;

    public Transform spawnPoint;
    public Image soldierIcon;
    public Vector3 iconStackPoint;

    [SerializeField]
    private Image progressBar;

    [SerializeField]
    private List<Image> unitIcons = new List<Image>();

    #region UIInfoDisplays  
    [Header("Info Panels")]
    private bool panel_Active = true;
    [SerializeField]
    private GameObject soldierPanel;
    [SerializeField]
    private GameObject pikemanPanel;
    [SerializeField]
    private GameObject shieldBearerPanel;
    #endregion


    // Use this for initialization
    private void Awake()
    {

        soldierPanel.SetActive(false);
        pikemanPanel.SetActive(false);
        shieldBearerPanel.SetActive(false);


    }



    // Update is called once per frame
    void Update () {


        if (spawnQ >= 1)
        {
            //Used to detemrine what to spawn next
            if (makeSolider == false)
            {
                DetermineNext();
            }

            timer += Time.deltaTime;
            if (timer >= defaultTimer)
            {
                timer = 0;
                EndTimer();
            }

            //Fill amount for progress bar
            progressBar.fillAmount = timer / defaultTimer;


        }


    }

    private void EndTimer()
    {
        //Set spawnPoint
        Vector3 spawnPosition = new Vector3(transform.position.x, 0.05f, -1f);
        //Spawn Unit
        Instantiate(nextUnit, spawnPosition, spawnPoint.rotation);
        //Reduce Q size
        spawnQ--;
        BarracksQueue.Remove(nextUnit); // remove so doesn't spam same unit

        makeSolider = false; // to recall determineUnitType

        //Deducte resource

        if (spawnQ >= 1)
        {
            timer = 0;
            Debug.Log(timer);
        }
        else
        {
            progressBar.enabled = false;
        }


        resources.UpdateInfo();
    }

    private void DetermineNext()
    {


        nextUnit = BarracksQueue.First();
        makeSolider = true;
    }

    //Add Soldier to queue
    public void SpawnSolider()
    {


        if (
            resources.Food >= soldierFoodCost &&
            resources.Wood >= soldierWoodCost &&
            resources.Stone >= soldierStoneCost &&
            resources.Metal >= soldierMetalCost )

        {
            if (BarracksQueue.Count < maxSpawnQ)
            {
                //Check Resources
                //remove resources
                resources.Food -= soldierFoodCost;
                resources.Wood -= soldierWoodCost;
                resources.Stone -= soldierStoneCost;
                resources.Metal -= soldierMetalCost;
                //update info
                //resources.UpdateInfo();


                BarracksQueue.Add(soldier);
                spawnQ++;


            }
        }
        else
        {
            Debug.Log("not enough resources");
        }

    }


    //Add Shieldbarer to queue
    public void SpawnShieldBarer()
    {
        if (
             resources.Wood >= shieldWoodCost 
            && resources.Food >= shieldFoodCost
            && resources.Stone >= shieldStoneCost
            && resources.Metal >= shieldMetalCost)
        {

            if (BarracksQueue.Count < maxSpawnQ)
            {
                //remove resources
                resources.Food -= shieldFoodCost;
                resources.Wood -= shieldWoodCost;
                resources.Stone -= shieldStoneCost;
                resources.Metal -= shieldMetalCost;
                //update info
                //resources.UpdateInfo();


                BarracksQueue.Add(shieldBarer);
                spawnQ++;

            }
        }
        else
        {
            Debug.Log("Not enough resources");

        }
    }

    //Add Pikeman to queue
    public void SpawnPikeman()
    {

        if (
             resources.Food >= pikeFoodCost
            && resources.Wood >= pikeWoodCost
            && resources.Stone >= pikeStoneCost
            && resources.Metal >= pikeMetalCost)
        {

            if (BarracksQueue.Count < maxSpawnQ)
            {

                //remove resources
                resources.Food -= pikeFoodCost;
                resources.Wood -= pikeWoodCost;
                resources.Stone -= pikeMetalCost;
                resources.Metal -= pikeMetalCost;
                //update info
                //resources.UpdateInfo();



                BarracksQueue.Add(pikeman);
                spawnQ++;


            }
        }
        else
        {
            Debug.Log("Not enough resources");
        }

    }


    #region InfoPanelsDisplay
    public void DisplaySoldierPanel()
    {

        soldierPanel.SetActive(panel_Active);
        panel_Active = !panel_Active;// changes  to opposite

    }


    public void DisplayPikemanPanel()
    {

        pikemanPanel.SetActive(panel_Active);
        panel_Active = !panel_Active;// changes  to opposite

    }


    public void DisplayShieldBearerPanel()
    {

        shieldBearerPanel.SetActive(panel_Active);
        panel_Active = !panel_Active;// changes  to opposite

    }

    #endregion



}

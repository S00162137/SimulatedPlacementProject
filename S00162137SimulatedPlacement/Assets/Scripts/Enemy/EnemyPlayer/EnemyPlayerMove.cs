using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerMove : MonoBehaviour {


    /* Priority

        Defend
            if building attacked, move to building, have units follow
            
        Collect
            if resources low and not under attack


        Build
            Build according to resources

        Make Units
            create set unit order piror


        Attack
            Create a unit follow script

            Retreat Value (Number of units before retreating)
            Attack Unit Values (Number of units before attacking)



    */

    //Used to determine where  to move to
     
    private GameObject townhallObject;
    public EnemyBuildingManager enemyBuildManager;




    #region Movement Variables
    public Vector2 targetPosition; // Final point to reach
    public bool MoveToTargetPoint = false;

    public Vector2 targetWayPoint; // alternative point
    public GameObject waypointObject;
    public bool MoveToWayPoint = false;


    //Facing direction variables
    public Vector3 ScaleRight;
    public Vector3 ScaleLeft;

    #endregion

    #region Priority Variables
    private float currentTimer = 0f;
    [SerializeField]
    private float PriorityCheckTimer = 5f;

    private bool Defending = false;
    private bool Collecting = false;
    private bool Building = false;
    private bool MakeUnits = false;
    private bool Attacking = false;

    public float collectionCD = 5f;

    public GameObject foodObj;
    public GameObject woodObj;
    public GameObject stoneVeinObj;

    #endregion




    public EnemyResources eResources; // resource checker
    public EnemyBuildingManager BuildingManager; // building checker
    // EnemyArmyManager
    public EnemyPlayerStats EPlayerStats;


    void Start () {

        //Detech if low on resources
        eResources = GameObject.FindGameObjectWithTag(Tags.ENEMY_CONTROLLER_TAG).GetComponent<EnemyResources>();
        enemyBuildManager = GameObject.FindGameObjectWithTag(Tags.ENEMY_CONTROLLER_TAG).GetComponent<EnemyBuildingManager>();


        //detect if buildings attacked
        BuildingManager = GameObject.FindGameObjectWithTag(Tags.ENEMY_CONTROLLER_TAG).GetComponent<EnemyBuildingManager>();
        townhallObject = BuildingManager.enemyTownhall;
        //Variables of this Player
        EPlayerStats = GetComponent<EnemyPlayerStats>();


        //Enemy Unit manager



        //Build controller


        //set facing Variables
        ScaleRight = transform.localScale;
        ScaleLeft = new Vector3(-ScaleRight.x, ScaleRight.y, ScaleRight.z);

        //FOR TESTING PURPOSES
        //DetermineWayPoint(new Vector2(47, transform.position.y - 20f));



        CheckPriorities();


	}
	
	void Update ()
    {

        currentTimer += Time.deltaTime;
        if (currentTimer >= PriorityCheckTimer)
        {
            CheckPriorities();
        }


        //Defend
        if (Defending == true)
        {
            //if no waypoint
            if (MoveToWayPoint == false)
            {
                //Move to targetPos


                transform.position = Vector2.MoveTowards(transform.position, targetPosition, EPlayerStats.moveSpeed / 100);
                if (Vector2.Distance(transform.position, targetPosition) < 1f)
                {
                    DefendBuilding();
                }

            }
            //If waypoint, move to waypoint
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, targetWayPoint, EPlayerStats.moveSpeed / 100);




                if (Vector2.Distance(transform.position, targetWayPoint) < 1f)
                {

                    UndergroundEntrance waypointDest = waypointObject.GetComponent<UndergroundEntrance>();
                    //Move Player to exit obj's position
                    transform.position = new Vector2(transform.position.x, waypointDest.exitObj.transform.position.y);
                    MoveToWayPoint = false;

                    #region Facing Direction
                    if (transform.position.x < targetPosition.x)
                    {
                        transform.localScale = ScaleLeft;
                    }
                    else
                    {
                        transform.localScale = ScaleRight;

                    }
                    #endregion
                }


            }


        }

        //Not defending
        else
        {
            if (Collecting == true)
            {
                //if no waypoint
                if (MoveToWayPoint == false)
                {
                    //Move to targetPos


                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, EPlayerStats.moveSpeed / 100);
                    if (Vector2.Distance(transform.position, targetPosition) < 1.5f)
                    {

                        FindFood();

                    }

                }
                //If waypoint, move to waypoint
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetWayPoint, EPlayerStats.moveSpeed / 100);




                    if (Vector2.Distance(transform.position, targetWayPoint) < 1f)
                    {

                        UndergroundEntrance waypointDest = waypointObject.GetComponent<UndergroundEntrance>();
                        //Move Player to exit obj's position
                        transform.position = new Vector2(transform.position.x, waypointDest.exitObj.transform.position.y);
                        MoveToWayPoint = false;

                        #region Facing Direction
                        if (transform.position.x < targetPosition.x)
                        {
                            transform.localScale = ScaleLeft;
                        }
                        else
                        {
                            transform.localScale = ScaleRight;

                        }
                        #endregion
                    }


                }
            }
            else
            {


            }
        }



        #region Base AI Code

        /*

        //if no waypoint
        if (MoveToWayPoint == false)
        {
            //Move to targetPos


            transform.position = Vector2.MoveTowards(transform.position, targetPosition, EPlayerStats.moveSpeed / 100);
            if(Vector2.Distance(Transform.position, targetPosition) < 2f)
            {
            (Do Thing);
            }
        }
        //If waypoint, move to waypoint
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetWayPoint, EPlayerStats.moveSpeed / 100);

            if (Vector2.Distance(transform.position, targetWayPoint) < 1f)
            {

                UndergroundEntrance waypointDest = waypointObject.GetComponent<UndergroundEntrance>();
                //Move Player to exit obj's position
                transform.position = new Vector2(transform.position.x, waypointDest.exitObj.transform.position.y);
                MoveToWayPoint = false;

                #region Facing Direction
                if (transform.position.x < targetPosition.x)
                {
                    transform.localScale = ScaleLeft;
                }
                else
                {
                    transform.localScale = ScaleRight;
                }
                #endregion
            }
        }




        */
        #endregion

    }


    //For determining Priority
    public void CheckPriorities()
    {
        //reset check timer
        currentTimer = 0f;
        Debug.Log("prio checked");

        //If building Under attack
        if (BuildingManager.NeedToDefend == true)
        {
            DetermineWayPoint(BuildingManager.buildingUnderAttack.transform.position);
            
            Defending = true;
        }

        //If No building under attack
        else
        {
            //Checks if resources are low
            eResources.CheckResourceNeeds();


            //Resources Low
            if (eResources.resourcesLow == true)
            {
                FindResources();
                        
            }

            //Balanced resources
            else
            {
                //Checks if need to build
                eResources.CheckHighThreshHolds();
                //Needs to build
                if (eResources.shouldBuild == true)
                {
                    FindTileToBuildOn();
                }
                //Create Units
                else
                {
                    Debug.Log("prio bottom reached");

                    FindResources();

                }

            }



        }


    }


    //For Moving
    public void DetermineWayPoint(Vector2 targetPoint)
    {

        MoveToTargetPoint = false;

        #region Going to underground Entrances

        // If player aboveground and target below ground
        if (targetPoint.y < -5f && transform.position.y > -5f)
        {
            GameObject tempObj = null;

            //Find each gameobject entrance, check distance to, go to enarest one
            foreach (GameObject item in GameObject.FindGameObjectsWithTag(Tags.UNDERGROUND_ENTRANCE_TAG))
            {
                if (tempObj == null)
                {
                    tempObj = item;
                }
                else  // not null
                {
                        //Distance from player to new object,                                              Distance from player to current obj
                    if (Vector2.Distance(transform.position, item.transform.position) < Vector2.Distance(transform.position, tempObj.transform.position))
                    {
                        tempObj = item;
                    }
                }
            }
            //entrance's position x, object's postion y
            targetWayPoint = new Vector2(tempObj.transform.position.x, transform.position.y);
            waypointObject = tempObj; // used to get script later
            MoveToWayPoint = true;

        }

        // If player undeground and target above ground
        else if (targetPoint.y > -5f && transform.position.y < -5f)
        {
            GameObject tempObj = null;

            //Find each gameobject entrance, check distance to, go to enarest one
            foreach (GameObject item in GameObject.FindGameObjectsWithTag(Tags.UNDERGROUND_ENTRANCE_TAG))
            {
                if (tempObj == null)
                {
                    tempObj = item;
                }
                else  // not null
                {
                    //Distance from player to new object,                                              Distance from player to current obj
                    if (Vector2.Distance(transform.position, item.transform.position) < Vector2.Distance(transform.position, tempObj.transform.position))
                    {
                        tempObj = item;
                    }
                }
            }
            //entrance's position x, object's postion y
            targetWayPoint = new Vector2(tempObj.transform.position.x, transform.position.y);
            waypointObject = tempObj; // used to get script later

            MoveToWayPoint = true;

        }


        #endregion

        // target and player on same level

        targetPosition = new Vector2(targetPoint.x, transform.position.y);

        //If on same level, immediately move to target pos
        if (MoveToWayPoint == false)
        {
            MoveToTargetPoint = true;
        }

        #region Facing Direction
        if (transform.position.x < targetPosition.x)
        {
            transform.localScale = ScaleLeft;
        }
        else
        {
            transform.localScale = ScaleRight;

        }
        #endregion  





    }



    #region Combat Related Methods

    public void DefendBuilding()
    {

        //Create ray cast left and right, if hit Milita or player, move to and attack


    }

    //Used for having Units follow or advance
    public void GatherUnits()
    {

    }

    public void AttackEnemy()
    {

    }

    public void RetreatHome()
    {

    }

    #endregion region

    #region BuildingMethods

    public void FindTileToBuildOn()
    {

        GameObject tempObj = null;
        GroundTile tempGTile = null;
        //Check all ground tiles, find any with nothing on it
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(Tags.GROUND_TAG))
        {
            //Find ground tile with no obj to go to
            GroundTile gTile = item.GetComponent<GroundTile>();
            if (gTile.hasItem == false)
            {
                if (tempObj == null)
                {
                    tempObj = item;
                    tempGTile = gTile;
                }
                else 
                {
                    if (Vector2.Distance(transform.position, tempObj.transform.position) > Vector2.Distance(transform.position, item.transform.position))
                    {
                        tempObj = item;
                        tempGTile = gTile;

                    }
                }
                //target potion =  tileobj,
                //if in range, place building on tiel
            }
        }

        if (Vector2.Distance(transform.position, tempObj.transform.position) < 1.5f)
        {
            BuildBuilding(tempGTile);
        }


        DetermineWayPoint(tempObj.transform.position);

    }



    public void BuildBuilding(GroundTile GroundToBuildOn)
    {

        GameObject buildingToPlace= null;

        #region Nested if's to prioritise what to build
        //Prio House
        if (eResources.needHouses == true)
        {
            buildingToPlace = enemyBuildManager.HousePrefab;
        }

        else
        {
            //Priority Farms
            if (eResources.needFarms == true)
            {
                buildingToPlace = enemyBuildManager.FarmPrefab;
            }
            
            else
            {
                //Priority Lumber
                if (eResources.needlumberMill == true)
                {
                    buildingToPlace = enemyBuildManager.LumbermillPrefab;
                }
                else
                {
                    //need barracks
                    if (eResources.needBarracks == true)
                    {
                        buildingToPlace = enemyBuildManager.BarracksPrefab;
                    }
                }
            }
        }
        #endregion

        Debug.Log(buildingToPlace.name);
        Instantiate(buildingToPlace, GroundToBuildOn.BuildPoint.position, buildingToPlace.transform.rotation);

    }


    #endregion

    #region InteractWithResources

    public void FindResources()
    {

        Collecting = true;
        if (eResources.lookForFood == true)
        {
            FindFood();
            Debug.Log("Looking for food");
        }
        else if (eResources.lookForWood == true)
        {
            FindTrees();
            Debug.Log("Looking for wood");

        }
        else if (eResources.lookForMetal == true || eResources.lookForStone == true)
        {
            FindStoneVein();
            Debug.Log("Looking for stone");

        }

    }


    public void FindFood()
    {
        FoodObject foodobjScript = null;

        //Find closest Food Object
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(Tags.FOOD_OBJ))
        {
            if (foodObj == null)
            {
                foodObj = item;
                foodobjScript = foodObj.GetComponent<FoodObject>();

            }
            else if (Vector2.Distance(transform.position, foodObj.transform.position) > Vector2.Distance(transform.position, item.transform.position))
            {
                foodObj = item;
                foodobjScript = foodObj.GetComponent<FoodObject>();

            }
        }


        if (foodObj != null)
        {
            targetPosition = foodObj.transform.position;

        }

        DetermineWayPoint(targetPosition);

        //If in range of the thing
        if (Vector2.Distance(transform.position, targetPosition) < 1.5f)
        {

            if (collectionCD > 0)
            {

                collectionCD -= 0.01f;
                if (collectionCD <=0)
                {
                    collectionCD = 5f;
                    foodobjScript.remainingAmount--; // Deduct food obj,

                    //increase resources
                    eResources.Food += (foodobjScript.NumberOfFoodToSpawn * 4);

               }
            }

        }

    }

    public void FindStoneVein()
    {
        rockVeins MineralVein = null;

        //Find closest Food Object
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(Tags.FOOD_OBJ))
        {
            if (stoneVeinObj == null)
            {
                stoneVeinObj = item;
                MineralVein = foodObj.GetComponent<rockVeins>();
            }
            else if (Vector2.Distance(transform.position, stoneVeinObj.transform.position) > Vector2.Distance(transform.position, item.transform.position))
            {
                stoneVeinObj = item;
                MineralVein = foodObj.GetComponent<rockVeins>();
                targetPosition = item.transform.position;

            }
        }


        DetermineWayPoint(targetPosition);

        //If in range of the thing
        if (Vector2.Distance(transform.position, targetPosition) < 1.5f)
        {

            if (collectionCD > 0)
            {

                collectionCD -= 0.01f;
                if (collectionCD <= 0)
                {
                    collectionCD = 5f;
                    MineralVein.remainingAmount--; // Deduct food obj,

                    //increase resources
                    eResources.Stone += MineralVein.NumberOfStonesToSpawn;
                    eResources.Metal +=MineralVein.NumberOfMetalToSpawn;

                }
            }

        }

    }

    public void FindTrees()
    {
        Tree TreeScript = null;

        //Find closest Food Object
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(Tags.TREES_TAG))
        {
            if (woodObj == null)
            {
                woodObj = item;
                TreeScript = woodObj.GetComponent<Tree>();
                targetPosition = item.transform.position;

            }
            else if (Vector2.Distance(transform.position, woodObj.transform.position) > Vector2.Distance(transform.position, item.transform.position))
            {
                woodObj = item;
                TreeScript = woodObj.GetComponent<Tree>();
                targetPosition = woodObj.transform.position;

            }
        }



        if (woodObj != null)
        {
            targetPosition = woodObj.transform.position;

        }



        //Move to targpetPoint
        DetermineWayPoint(targetPosition);



        //If in range, gather resources
        if (Vector2.Distance(transform.position, targetPosition) < 1.5f)
        {
            if (collectionCD > 0)
            {
                collectionCD -= 0.01f;
                if (collectionCD <= 0)
                {
                    collectionCD = 5f;
                    TreeScript.remainingAmount--; // Deduct food obj,

                    //increase resources
                    eResources.Wood += (TreeScript.NumberOfWoodObjsToSpawn * 4);
                }
            }
        }


    }



    #endregion


    #region Unit Gen Methods

    public void MakeVillager()
    {
        if (eResources.Food > eResources.HighFoodCap)
        {
            //Move to
            DetermineWayPoint(townhallObject.transform.position);


            if (Vector2.Distance(transform.position, townhallObject.transform.position) < 1.5f)
            {
                townhallObject.GetComponent<EnemyTownhall>().MakeNewVillager();
            }
            CheckPriorities();
        }


    }

    public void MakeUnit()
    {
        if (eResources.Food > eResources.HighFoodCap)
        {
            //Move to Barracks
            //DetermineWayPoint(townhallObject.transform.position);

            //barracks etc
            if (Vector2.Distance(transform.position, townhallObject.transform.position) < 1.5f)
            {

            }
            CheckPriorities();
        }

    }


    #endregion

}

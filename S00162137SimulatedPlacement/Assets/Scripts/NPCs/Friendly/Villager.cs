using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour {


    private VillagerStats villagerStats;

    //Working
    public GameObject workingBuilding;
    public bool working = false;
    public bool atBuilding = false;
    public Vector2 wanderPointLeft;
    public Vector2 wanderPointRight;

    //Wandering
    [Header("Movement Variables")]
    public Vector2 assignedTargetPos;
    public Vector2 wanderToPoint;
    [SerializeField]
    private bool wandertopointSet = false;
    private float xScale;
    private float xScaleMinus;

    // Navigation Variables
    private GameObject wayPointObj;
    private Vector2 WayPointTarget;
    private bool movetoWaypoint = false;
    private Vector2 TargetPoint;


    private Vector2 townhallPoint;
    private Vector2 MaxWanderPoint;
    //RunningAway

    public Resources resources;


	// Use this for initialization
	void Start () {

        xScale = transform.localScale.x;
        xScaleMinus = -xScale;



        resources = GameObject.FindGameObjectWithTag(Tags.PLAYER_CONTROLLER_TAG).GetComponent<Resources>();

        villagerStats = GetComponent<VillagerStats>();
        resources.People++;

        UnEmployed();
        RemoveFromBuilding();
	}
	

    private void FixedUpdate()
    {
        //Employed
        if (working == true && atBuilding == false)
        {

            DetermineWayPoint(workingBuilding.transform.position);
            //move to waypoint
            if (movetoWaypoint == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(WayPointTarget.x, transform.position.y),
                 villagerStats.MoveSpeed / 100);
                
            }
            //move to building
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(workingBuilding.transform.position.x, transform.position.y),
                 villagerStats.MoveSpeed / 100);
            }

            if (Vector2.Distance(transform.position, workingBuilding.transform.position) < 1f)
            {
                FindWanderPoint();

                atBuilding = true;
            }
        } // end Employed




        if (atBuilding == true)
        {
            if (wandertopointSet == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, wanderToPoint, villagerStats.MoveSpeed / 100);
                if (Vector2.Distance(transform.position, wanderToPoint) < 1.5)
                {
                    wandertopointSet = false;
                }
            }
            else
            {
                FindWanderPoint();
            }
        }

        //Unemployed
        else if (working == false)
        {
            if (wandertopointSet == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, wanderToPoint, villagerStats.MoveSpeed / 100);
                if (Vector2.Distance(transform.position, wanderToPoint) < 1)
                {
                    wandertopointSet = false;
                }
            }
            else
            {
                FindWanderPoint();
            }
        }// end unemployed


    } 

    public void AssignToBuilding(GameObject building)
    {
        workingBuilding = building;
        wanderPointLeft = new Vector2(workingBuilding.transform.position.x - 2.5f, building.transform.position.y);
        wanderPointRight = new Vector2(workingBuilding.transform.position.x + 2.5f, building.transform.position.y);

        atBuilding = false;
        working = true;
        resources.Unemployed--;
        resources.UpdateInfo();

    }

    //Movement method
    public void DetermineWayPoint(Vector2 targetPoint)
    {

        movetoWaypoint = false;

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
            WayPointTarget = new Vector2(tempObj.transform.position.x, transform.position.y);
            wayPointObj = tempObj;
            movetoWaypoint = true;

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
            WayPointTarget = new Vector2(tempObj.transform.position.x, transform.position.y);
            wayPointObj = tempObj;
            movetoWaypoint = true;

        }

        //Use Waypoint
        if (movetoWaypoint ==true)
        {
            if (Vector2.Distance(transform.position, wayPointObj.transform.position) < 1.5f)
            {
                UndergroundEntrance waypointDest = wayPointObj.GetComponent<UndergroundEntrance>();
                transform.position = new Vector2(transform.position.x, waypointDest.exitObj.transform.position.y);
                movetoWaypoint = false;

            }
        }
        #endregion

        // target and player on same level
        wanderToPoint = new Vector2(targetPoint.x, transform.position.y);

        //If on same level, immediately move to target pos


        #region Facing Direction
        if (wanderToPoint.x < transform.position.x)
        {
            transform.localScale = new Vector3(xScaleMinus, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);

        }
        #endregion  





    }

    public void RemoveFromBuilding()
    {
        workingBuilding = null;
        FindMaxBuilding();
        wanderPointLeft = new Vector2(townhallPoint.x -2f, transform.position.y);
        wanderPointRight = new Vector2(MaxWanderPoint.x +2f, transform.position.y);

        FindWanderPoint();


        working = false;
    }

    public void FindMaxBuilding()
    {
        GameObject tempObj = null;

        foreach (GameObject item in GameObject.FindGameObjectsWithTag(Tags.BUILDING_TAG))
        {
            if (tempObj == null)
            {
                tempObj = item;
            }
            else
            {
                if (Vector2.Distance(tempObj.transform.position, item.transform.position) > Vector2.Distance(townhallPoint, tempObj.transform.position))
                {
                    tempObj = item;
                }
            }

        }

        MaxWanderPoint = new Vector2(tempObj.transform.position.x, transform.position.y);



    }

    public void FindWanderPoint()
    {

        wanderToPoint = new Vector2(Random.Range(wanderPointLeft.x, wanderPointRight.x), transform.position.y);

        if (wanderToPoint.x > transform.position.x)
        {
            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(xScaleMinus, transform.localScale.y, transform.localScale.z);

        }
        wandertopointSet = true;
    }


    public void Employed()
    {

        resources.Unemployed--;
        resources.UpdateInfo();
    }

    public void UnEmployed()
    {
        working = false;
        resources.Unemployed++;
        RemoveFromBuilding();
        resources.UpdateInfo();

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gibbler : MonoBehaviour {


    public GameObject townhall;

    public WaveManager waveManager;

    #region Hovel Variables
    public GameObject HovelFrom;
    public Vector2 hovelPosition; // starting point
    public HovelScript hovelScript;

    private bool hasHovel = false;

    #endregion

    public GibblerStats gibbleStats;

    [Header("Movement Variables")]
    #region Movement Variables
    public Vector2 targetPosition; // target to move towards
    public bool MoveToTargetPoint = false;
    public Vector2 targetWayPoint;

    public GameObject WaypointObject;
    public bool MoveToWayPoint = false;

    public bool attacking = false;


    public Vector3 xScalePositive;
    public Vector3 xScaleNegative;

    #endregion

    [Header("Combat Restraints")]
    public float combatDistance = 7f;
    public float HomeWanderDistance = 20f; //Max Distance Gibbler can wander from hovel out of combat
    public float HomeWanderDistanceMax = 25f; //Max Distance Gibbler can wander from hovel out of combat

    public float DistanceFromHovel; //General Distance Variable
    public float HomeCombatDistance; // Max Distance from hovel before gibbler goes back

    //targets
    [SerializeField]
    private LayerMask[] targetMasks;




    public bool inCombat = false; //If in combat
    private bool returningHome = false; // if too far from hovel
    private bool wanderingAround = true; //  if in range of hovel and not in combat
    public GameObject targetObj;

    private bool wanderPointsSet = false;
    public float wanderPointLeft;
    public float wanderPointRight;
    public Vector2 wanderToPoint;





	// Use this for initialization
	void Start () {

        // Origon wander point
        hovelPosition = transform.position;

        //Find Hovel spawned from
        foreach (GameObject tempHovel in GameObject.FindGameObjectsWithTag(Tags.GIBBLERHOVEL_TAG))
        {
            if (HovelFrom ==  null)
            {
                HovelFrom = tempHovel;
                hovelScript = tempHovel.GetComponent<HovelScript>();
                hasHovel = true;
            }
            else
            {
                if (
                    Vector2.Distance(tempHovel.transform.position, transform.position) 
                    < 
                    Vector2.Distance(HovelFrom.transform.position, transform.position)
                    )
                {
                    HovelFrom = tempHovel;
                    hovelScript = tempHovel.GetComponent<HovelScript>();
                    hasHovel = true;
                }

            }




        }

        //CircleC2D = GetComponent<CircleCollider2D>();

        //target destination
        townhall = GameObject.FindGameObjectWithTag(Tags.TOWNHALL_TAG);

        //stats
        gibbleStats = GetComponent<GibblerStats>();

        //Wave, when to go, collections
        waveManager = GameObject.FindGameObjectWithTag(Tags.ENEMY_CONTROLLER_TAG).GetComponent<WaveManager>();
        waveManager.Gibblers.Add(gameObject);

        //Facing Direcition
        xScalePositive = transform.localScale;
        xScaleNegative = new Vector3(-xScalePositive.x, transform.localScale.y, transform.localScale.z);
	}
	
	// Update is called once per frame


    private void FixedUpdate()
    {


        if (attacking == false)
        {
            if (HovelFrom != null)
            {
                DistanceFromHovel = Vector3.Distance(transform.position, HovelFrom.transform.position);
            }




            if (hasHovel == true)
            {
                #region movingAround
                if (inCombat == false)
                {
                    //returnback to hovel
                    if (DistanceFromHovel > HomeWanderDistanceMax && returningHome == false)
                    {
                        wanderingAround = false;
                        returningHome = true;
                        ReturnHome();

                    }
                    //Move back to within range
                    if (returningHome == true)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, hovelPosition, (gibbleStats.moveSpeed / 100));
                        if (DistanceFromHovel < HomeWanderDistance)
                        {
                            returningHome = false;
                            wanderingAround = true;
                        }
                    }

                    //Wandering
                    if (returningHome == false && wanderingAround == true)
                    {
                        if (wanderPointsSet == true)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, wanderToPoint, (gibbleStats.moveSpeed / 100));
                            if (Vector2.Distance(transform.position, wanderToPoint) < 1f)
                            {
                                wanderPointsSet = false;
                            }
                        }
                        //FindWander Point
                        else
                        {
                            FindWanderPoint();
                        }
                    }


                }


                #endregion

                #region COMBAT
                else //In combat
                {

                    wanderingAround = false;

                    if (returningHome == false)
                    {
                        //Moves towards the target
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetObj.transform.position.x, transform.position.y), (gibbleStats.moveSpeed / 100));

                        //Change FacingDir
                        if (targetObj.transform.position.x > transform.position.x)
                        {
                            transform.localScale = xScalePositive;
                        }
                        else
                        {
                            transform.localScale = xScaleNegative;

                        }


                        if (Vector2.Distance(gameObject.transform.position, targetObj.transform.position) > combatDistance)
                        {
                            returningHome = true;

                        }
                        else if (DistanceFromHovel > HomeCombatDistance)
                        {
                            returningHome = true;
                        }
                    }
                    else // if returninghome is true
                    {
                        transform.position = Vector2.MoveTowards(transform.position, hovelPosition, (gibbleStats.moveSpeed / 100));

                        //Change facing Dir
                        if (hovelPosition.x > transform.position.x)
                        {
                            transform.localScale = xScalePositive;
                        }
                        else
                        {
                            transform.localScale = xScaleNegative;

                        }


                        if (DistanceFromHovel < HomeWanderDistance)
                        {
                            returningHome = false;
                            inCombat = false;
                            wanderingAround = true;
                        }
                    }

                }

                #endregion

            }

        }
        //Attacking is true
        else
        {
            DetermineWayPoint(townhall.transform.position);

            //if no waypoint
            if (MoveToWayPoint == false)
            {
                //Move to targetPos


                transform.position = Vector2.MoveTowards(transform.position, targetPosition, gibbleStats.moveSpeed / 100);

            }
            //If waypoint, move to waypoint
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, targetWayPoint, gibbleStats.moveSpeed / 100);




                if (Vector2.Distance(transform.position, targetWayPoint) < 1f)
                {

                    UndergroundEntrance waypointDest = WaypointObject.GetComponent<UndergroundEntrance>();
                    //Move Player to exit obj's position
                    transform.position = new Vector2(transform.position.x, waypointDest.exitObj.transform.position.y);
                    MoveToWayPoint = false;

                    #region Facing Direction
                    if (transform.position.x < targetPosition.x)
                    {
                        transform.localScale = xScaleNegative;
                    }
                    else
                    {
                        transform.localScale = xScalePositive;

                    }
                    #endregion
                }


            }
        }

    }

    public void FindWanderPoint()
    { 
        wanderPointLeft  =   transform.position.x - 7f;
        wanderPointRight =   transform.position.x + 7f;

        wanderToPoint = new Vector2(Random.Range(wanderPointLeft, wanderPointRight), transform.position.y) ;
        //Change facing dir
        if (wanderToPoint.x > transform.position.x)
        {
            transform.localScale = xScalePositive;
        }
        else
        {
            transform.localScale = xScaleNegative;

        }

        wanderPointsSet = true;
    }

    public void ReturnHome()
    {


        wanderToPoint = new Vector2(HovelFrom.transform.position.x, transform.position.y);
        if (wanderToPoint.x > transform.position.x)
        {
            transform.localScale = xScalePositive;
        }
        else
        {
            transform.localScale = xScaleNegative; ;

        }

    }

    public void FindPlayerPoint()
    {

    }

    public void AssignToHovel(GameObject hovel)
    {

        HovelFrom = hovel;
        hovelScript = HovelFrom.GetComponent<HovelScript>();
        hasHovel = true;

    }

    //Movement method
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
            WaypointObject = tempObj; // used to get script later
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
            WaypointObject = tempObj; // used to get script later

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
        if ( targetPosition.x < transform.position.x)
        {
            transform.localScale = xScaleNegative;
        }
        else
        {
            transform.localScale = xScalePositive;

        }
        #endregion  





    }


    private void OnCollisionEnter2D(Collision2D collision)
    {



        if (collision.gameObject.tag == Tags.PLAYER_TAG)
        {
            PlayerStats pstats= collision.gameObject.GetComponent<PlayerStats>();

            //knockback player
            pstats.KnockedBack(gibbleStats.KnockbackForce, transform.position);


            //Apply damage to player
            pstats.TakeDamage(gibbleStats.Power);
         

        }


       
        //Collide with Milita
        else if (collision.gameObject.tag == Tags.MILITIA_TAG)
        {
            MilitiaStats mstats = collision.gameObject.GetComponent<MilitiaStats>();

            //Knockback
            mstats.KnockedBack(gibbleStats.KnockbackForce, transform.position);
            gibbleStats.Knockedback(mstats.KnockBackForce, collision.gameObject.transform.position);
           
            //Deal damage
            mstats.TakeDamage(gibbleStats.Power);
            gibbleStats.TakeDamage(mstats.Power);

        }

        //Collide with Building
        else if (collision.gameObject.tag == Tags.BUILDING_TAG)
        {
            BuildingStats bstats = collision.gameObject.GetComponent<BuildingStats>();

            //Knockback
            gibbleStats.Knockedback(gibbleStats.KnockbackForce, collision.gameObject.transform.position);

            //Deal damage
            bstats.CurrentHealth(gibbleStats.Power);
            gibbleStats.TakeDamage(bstats.buildingDefence);

        }



        //Collide with border
        else if (collision.gameObject.tag == Tags.BORDER_TAG)
        {
            //Reset if touches border
            if (collision.gameObject.transform.position.x < gameObject.transform.position.x)
            {
                ReturnHome();
            }

            else if (collision.gameObject.transform.position.x > gameObject.transform.position.x)
            {
                ReturnHome();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == Tags.GIBBLERHOVEL_TAG)
        //{
        //    CircleC2D.isTrigger = false;
        //    if (hasHovel == false)
        //    {
        //        HovelFrom = collision.gameObject;
        //        hovelPosition = HovelFrom.transform.position;
        //        gibbleStats.homeHovel = hovelScript;
        //        //Call assign hovel objects
        //        AssignToHovel();

        //    }


        //}





    }
}

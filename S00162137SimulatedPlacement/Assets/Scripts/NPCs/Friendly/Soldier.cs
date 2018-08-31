using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {


    
    [HideInInspector]
    public MilitiaStats UnitStats;
    public enum UnitType {Soldier, Pikeman, Shieldbearer, Archer };
    public UnitType unittype;

    [Header("Combat Variables")]
    [SerializeField]
    private string[] targetTags;
    [SerializeField]
    private List<GameObject> EnemiesInRange = new List<GameObject>();
    [SerializeField]
    private GameObject targetEnemy = null;
    [SerializeField]
    private float DistanceFromPlayer;
    [SerializeField]
    private float MaxDistanceFromPlayer;

    //Movement Variables

    #region Following Variables
    [Header("Movement Variables")]
    public Vector2 targetDesination;
    public bool MoveToTarget = false;
    public Vector2 WayPointDestination;
    public bool MoveToWayPoint = false;


    public bool Following = false;
    [SerializeField]
    private GameObject FollowObj;

    #endregion

    public bool inCombat = false;
    public bool Advancing = false;
    public bool Retreating;
    public bool advancinvgRight = true;

    //Used for healing
    public bool AtRallyPoint = false;
    public Vector2 advanceDir;
    //Used to determine 
    public Vector2 RallyDir;

    //private float Speed = 4f;

    //Ued to be added to List
    private MilitiaManager MilitiaM;

    Transform tform;
    GameObject RallyPoint;

    //Dir Facing Variables
    private Vector3 goingRightScale;
    private Vector3 goingLeftScale;


	// Use this for initialization
	void Start () {

   
    
        //Units stats EG health and pwoer
        UnitStats = gameObject.GetComponent<MilitiaStats>();
        //dir moving in
        advanceDir = gameObject.transform.right;


        //Rally point Obj
        RallyPoint = GameObject.FindGameObjectWithTag(Tags.RALLYPOINT_TAG);


        Retreating = true;

        //For visual Movement
        goingRightScale = gameObject.transform.localScale;
        goingLeftScale = new Vector3(-goingRightScale.x,goingRightScale.y, goingRightScale.x);


        //Used for Rallying and Advancing
        MilitiaM = GameObject.FindGameObjectWithTag(Tags.PLAYER_CONTROLLER_TAG).GetComponent<MilitiaManager>();


        tform = GetComponent<Transform>();

        MilitiaM.Militia.Add(gameObject); // add this to list of military units
        //Add to soldiers
        if (unittype == UnitType.Soldier)
        {
            MilitiaM.SoldiersList.Add(gameObject); // add this to list of military units

        }
        //Add to Pikeman
        else if(unittype == UnitType.Pikeman)
        {
            MilitiaM.PikemenList.Add(gameObject); // add this to list of military units

        }
        //Add to shieldBearers
        else if (unittype == UnitType.Shieldbearer)
        {
            MilitiaM.ShieldBearerList.Add(gameObject); // add this to list of military units

        }
        //Add to archers
        else if (unittype == UnitType.Archer)
        {
            MilitiaM.ArcherList.Add(gameObject); // add this to list of military units

        }




        //Assign the player
        FollowObj = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);

    }

    // Update is called once per frame
    void Update ()
    {

        //Moving forward
        if (Advancing == true)
        {
            gameObject.transform.Translate((new Vector3(1f, 0, 0) * UnitStats.MoveSpeed) *Time.deltaTime);


        }

        //retreating
        else
        {

            //Used when the player want's to follow
            if (Following == true)
            {

                //To Check Range from player
                DistanceFromPlayer = Vector2.Distance(transform.position, FollowObj.transform.position);

                if (inCombat == true)
                {
                    if (DistanceFromPlayer > MaxDistanceFromPlayer)
                    {
                        inCombat = false;
                    }
                    //Charge enemy
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetEnemy.transform.position.x, transform.position.y), UnitStats.MoveSpeed / 60);
                    FindTargetEnemy();
                }

                //If player too far or not in combat, follow player
                else
                {
                    //Follow Player
                    DetermineMoveDir(FollowObj.transform.position);
                    if (MoveToWayPoint == true)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, WayPointDestination, UnitStats.MoveSpeed / 60);
                        if (Vector2.Distance(transform.position, WayPointDestination) < 1f)
                        {
                            gameObject.transform.position = new Vector2(transform.position.x, FollowObj.transform.position.y);
                            DetermineMoveDir(FollowObj.transform.position);
                            MoveToWayPoint = false;

                        }

                    }

                    // OnSameLevel
                    else
                    {
                        transform.position = Vector2.MoveTowards(transform.position, targetDesination, UnitStats.MoveSpeed / 60);
                    }
                }
               
            }

            //Other wise Returns to rallypoint
            else
            {
                if (Vector2.Distance(gameObject.transform.position, RallyPoint.transform.position) < 2)
                {
                    AtRallyPoint = true;
                }
                else
                {
                    DetermineDirToRally();
                    //move to rally point dir
                    gameObject.transform.Translate((RallyDir * UnitStats.MoveSpeed) * Time.deltaTime);

                }
                
            }

        }// End Retreating

    }


    #region General Movement Methods

    public void DetermineDirToRally()
    {
        //rallyPoint is to the left
        if (gameObject.transform.position.x < RallyPoint.transform.position.x )
        {
            RallyDir = Vector2.right;
            gameObject.transform.localScale = goingRightScale;
        }

        //rallypoint is to the right
        else
        {
            RallyDir = Vector2.left;
            gameObject.transform.localScale = goingLeftScale;
        }

    }

    public void FollowCommand()
    {
        Retreating = false;

        Following = !Following;
        if (Following == false)
        {
            DetermineMoveDir(GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform.position);

        }

    }

    public void DetermineMoveDir(Vector2 targetposition)
    {
        MoveToTarget = false;

        #region MoveTo UndergroundEntrances

        //If above ground and target below
        if (targetposition.y < -5f && transform.position.y > -5f)
        {
            GameObject tempEntrance = null;

            foreach (GameObject entrance in GameObject.FindGameObjectsWithTag(Tags.UNDERGROUND_ENTRANCE_TAG))
            {
                if (tempEntrance == null)
                {
                    tempEntrance = entrance;
                }
                else
                {
                    //If distance is less than current entrance
                    if (Vector2.Distance(transform.position, entrance.transform.position) < Vector2.Distance(transform.position,tempEntrance.transform.position ))
                    {
                        tempEntrance = entrance;
                    }
                }
            }
            WayPointDestination = new Vector2(tempEntrance.transform.position.x, transform.position.y);
            MoveToWayPoint = true;
            //Assign so it goes to entrance


        }
        else if (targetposition.y > -5f && transform.position.y < -5f)
        {
            GameObject tempEntrance = null;

            foreach (GameObject entrance in GameObject.FindGameObjectsWithTag(Tags.UNDERGROUND_ENTRANCE_TAG))
            {
                if (tempEntrance == null)
                {
                    tempEntrance = entrance;
                }
                else
                {
                    //If distance is less than current entrance
                    if (Vector2.Distance(transform.position, entrance.transform.position) < Vector2.Distance(transform.position, tempEntrance.transform.position))
                    {
                        tempEntrance = entrance;
                    }
                }
            }
            WayPointDestination = new Vector2(tempEntrance.transform.position.x, transform.position.y);
            MoveToWayPoint = true;
            //Assign so it goes to entrance

        }

        #endregion

        targetDesination = new Vector2(targetposition.x, transform.position.y);
        //If on same level
        if (MoveToWayPoint == false)
        {
            MoveToTarget = true;
        }


        //Facing Direction
        if (transform.position.x < targetposition.x)
        {
            transform.localScale = goingRightScale;
        }
        else
        {
            transform.localScale = goingLeftScale;
        }
    }

    #endregion

    //Find target
    public void FindTargetEnemy()
    {
        //Temp variable

        foreach (GameObject item in EnemiesInRange)
        {
 
                if (targetEnemy != null)
                {

                    if (Vector2.Distance(transform.position, item.transform.position)
                        <
                        Vector2.Distance(transform.position, targetEnemy.transform.position))
                    { targetEnemy = item; }

                }

            

        }


       // if (targetEnemy.transform.position.x < transform.position.x)
       // {
       //     transform.localScale = goingRightScale;
       // }
       // else
       // {
       //     transform.localScale = goingLeftScale;

       //}

    }

    //Add to combat List
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string tag in targetTags)
        {
            if (collision.gameObject.tag == tag)
            {
                EnemiesInRange.Add(collision.gameObject);
                //FindTargetEnemy();
                targetEnemy = collision.gameObject;
                inCombat = true;
            }

        }
    }
    //Remove and exit combat
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject enemy in EnemiesInRange)
        {
            if (enemy == collision.gameObject )
            {
                EnemiesInRange.Remove(enemy);
            }
        }
        if (EnemiesInRange.Count <= 0)
        {
            inCombat = false;
        }

    }




}

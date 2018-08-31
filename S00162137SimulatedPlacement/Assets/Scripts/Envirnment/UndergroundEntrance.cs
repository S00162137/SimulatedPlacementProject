using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UndergroundEntrance : MonoBehaviour {

    [SerializeField]
    private GameObject showText;

    private GameObject playerObj; //Player
    public GameObject entranceObj; // This Object
    public GameObject exitObj; // where the player will end up

    //This ground tile
    public GroundTile gTile;
    public Vector3 pointDir; // direction Raycast goes

    [SerializeField]
    private bool AimUp = false; // to determine raycast Dir
    [SerializeField]
    private LayerMask targetMask; // Layermask for raycast

    public bool playerAtEntrance = false; // if player can use entrance
    public CameraFollow cfollow;

	// Use this for initialization
	void Start () {


        gTile = GetComponent<GroundTile>();
        FindEntranceExit();
        cfollow = Camera.main.GetComponent<CameraFollow>();
        showText.SetActive(playerAtEntrance);


    }

    // Update is called once per frame
    void Update () {

        if (playerAtEntrance == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MovePlayer();
            }
        }
	}

     private void FindEntranceExit()
    {
        //Determines direction RayCast goes
        if (AimUp == true)
        {
            pointDir = Vector2.up;
        }
        else
        {
            pointDir = Vector2.down;
        }

        //Draws Raycast, if hits groundtype
        RaycastHit2D hit2d = Physics2D.Raycast(gameObject.transform.position, pointDir, 1000, targetMask);
        if (hit2d.collider != null)
        {

            if (hit2d.collider.tag == Tags.UNDERGROUND_ENTRANCE_TAG)
            {
                //if object has entrance script
                if (hit2d.collider.gameObject.GetComponent<UndergroundEntrance>())
                {

                    //sets exitobj
                    UndergroundEntrance ugEntranceScript = hit2d.collider.GetComponent<UndergroundEntrance>();

                    exitObj = ugEntranceScript.entranceObj;
                    ugEntranceScript.exitObj = entranceObj; // if not set yet set it's exitobj to this scri[t's exit script

                }

            }
        }
        else
        {
            Debug.Log(gameObject.name + "null");
        }


    }





    //Teleport player to exit
    private void MovePlayer()
    {
        playerObj.transform.position = exitObj.transform.position;
        cfollow.ChangeBackground(); // change background image
        ClearPlayer();
    }

    public void ClearPlayer()
    {
        playerObj = null;
        playerAtEntrance = false;
        showText.SetActive(playerAtEntrance);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.PLAYER_TAG)
        {
            playerAtEntrance = true;
            showText.SetActive(playerAtEntrance);
            playerObj = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.PLAYER_TAG)
        {
            ClearPlayer();
        }
    }

}

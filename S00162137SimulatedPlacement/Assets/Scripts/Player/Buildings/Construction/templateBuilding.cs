using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class templateBuilding : MonoBehaviour {

    BuildPanelManager BPM;


    public int WoodCost;
    public int StoneCost;
    public int MetalCost;
    public int PeopleCost;

    public GameObject BuildingToConstruct;

    public bool ableToPlace = false;
    public bool enoughResources = false;


    //error showing
    public SpriteRenderer templateSpriteRender;

    [SerializeField]
    private Color Canplacecolor;
    [SerializeField]
    private Color CantplaceColor;


    //Needed to taget ground with raycast
    [SerializeField]
    private LayerMask targetMaskLayer;
    private GroundTile gTile;
    [SerializeField]
    private GroundTile.TileType targetGroundType;

    private Resources resources;

    public Text errortext;

	// Use this for initialization
	void Start () {


        BPM = GameObject.Find(Tags.SCENECONTROLER_TAG).GetComponent<BuildPanelManager>();
        resources = GameObject.FindGameObjectWithTag(Tags.PLAYER_CONTROLLER_TAG).GetComponent<Resources>();
        errortext = GameObject.Find(Tags.DirErrorMessageText).GetComponent<Text>();// to display error messages
        templateSpriteRender = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        //Gets mouse position to world
        Vector2 housePos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 housePosWorld = new Vector3(housePos.x, housePos.y, 1);
        gameObject.transform.position = housePosWorld;

      
        RaycastHit2D hit2d = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 100, targetMaskLayer);
        if (hit2d.collider != null)
        {
            //Ray to detemrine if object can be placed there
            if (hit2d.collider.tag == Tags.GROUND_TAG)
            {

                //Get the groundtile Script from hit tile
                gTile = hit2d.collider.GetComponent<GroundTile>();

                if (gTile != null)
                {

                    if (gTile.tType == targetGroundType || targetGroundType == GroundTile.TileType.Both)
                    {


                        //If Trees or Rocks in way
                        if (gTile.hasItem == true)
                        {
                            ableToPlace = false;

                        }
                        //If cleared patch
                        else
                        {
                            ableToPlace = true;

                        }
                    }
                    else
                    {
                        ableToPlace = false;
                    }



                }
            }
        }
    


        //Check if enough resources
        if (resources.Wood >= WoodCost
            && resources.Stone >= StoneCost
            && resources.Unemployed >= PeopleCost)
        {
            enoughResources = true;
        }
        else
        {
            enoughResources = false;
        }


        //Change color of templateObject
        if (enoughResources == true && ableToPlace == true)
        {
            templateSpriteRender.color = Canplacecolor;

        }
        else
        {
            templateSpriteRender.color = CantplaceColor;

        }

        //RaycastHit2D hit2d = Physics2D.Raycast(transform.position, Vector2.down, 100, targetMaskLayer);
        //On left click
        if (Input.GetMouseButtonDown(0))
        {
            if (enoughResources == true)
            {

                //Create House
                if (ableToPlace == true)
                { 


                    //   Instantiate(BuildingToConstruct, new Vector3(housePosWorld.x, -0.11f, housePosWorld.z), transform.rotation);
                    Instantiate(BuildingToConstruct, new Vector3(gTile.BuildPoint.position.x, gTile.BuildPoint.position.y, 1f), transform.rotation);
                    //deduct resources
                    resources.Wood -= WoodCost;
                    resources.Stone -= StoneCost;
                    resources.Unemployed -= PeopleCost;
                    BPM.templateExists = false; // So you can build again
                    Destroy(gameObject);
                }
                else
                {
                    errortext.text = "Can't build there!";
                }

            }
            else
            {
                errortext.text = "Not Enough Resources!";
            }
       

        }

        else if (Input.GetMouseButtonDown(1))
        {
            BPM.templateExists = false; // can build again

            Destroy(gameObject);
        }
        


     

    }


   


}

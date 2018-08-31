using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tree : MonoBehaviour {

    private float progressAmount = 0; // Player's progress on hitting
    private float progressmax = 100f;

    public float remainingAmount;

    //Objects
    [SerializeField]
    private GameObject woodObj;
    public int NumberOfWoodObjsToSpawn = 4;
   
    //Ui Elements
    [SerializeField]
    private GameObject canvasBar;
    [SerializeField]
    private Image durabilityBar;
    private Text remainSourcesText;

    private GroundTile gtile;
    [SerializeField]
    private LayerMask targetMaskLayer;



    // Use this for initialization
    void Start () {
        canvasBar.SetActive(false);

        //Used to determine if can build on this spot or not
        RaycastHit2D hit2d = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 100, targetMaskLayer);
        if (hit2d.collider.tag == Tags.GROUND_TAG)
        {

            gtile = hit2d.collider.GetComponent<GroundTile>();
            if (gtile != null)
            {
                gtile.hasItem = true;
                gtile.TileObj = gameObject;
            }
        }


    }

    //Show Progress bar
    private void OnMouseOver()
    {
        canvasBar.SetActive(true);

    }
    private void OnMouseExit()
    {
        canvasBar.SetActive(false);
    }


    private void ChangeProgress(float amount)
    {
        progressAmount += amount;

        if (progressAmount >= progressmax)
        {
            GenerateResources();
            remainingAmount--;

            if (remainingAmount == 0)
            {
                //No Resources Left
                NoResourcesLeft();
            }
            else
                progressAmount = 0;


        }
        //Show Progress
        durabilityBar.fillAmount = progressAmount / progressmax;

    }

    //Drop wood on ground
    private void GenerateResources()
    {
        for (int i = 0; i < NumberOfWoodObjsToSpawn; i++)
        {
            Vector2 metalPos = new Vector3((gameObject.transform.position.x + Random.Range(-2, 2)), transform.position.y + 1f, -1f);
            Instantiate(woodObj, metalPos, woodObj.transform.rotation);
        }

    
    }

    //Destorying Object
    private void NoResourcesLeft()
    {
        //Clear tile obj so can place object
        gtile.hasItem = false;
        gtile.TileObj = null;


        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.PLAYERWEAPON_TAG)
        {
            WeaponScript playerWeapon = collision.gameObject.GetComponent<WeaponScript>();
            //Determine damage taken
            if (playerWeapon.equippedWeapon == WeaponScript.WeaponType.Axe)
            {
                ChangeProgress(10);
            }
            else
            {
                ChangeProgress(5);
            }



        }
    }
}

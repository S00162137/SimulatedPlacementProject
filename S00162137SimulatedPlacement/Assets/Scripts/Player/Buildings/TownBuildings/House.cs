using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour {

    //Doesn't inherit from Building as has different functions

    public int Health = 100;
    private int MaxHealth;
    public int HousingBonus =5;

    #region Upgrade Variables
    //Upgrade Variables
    public SpriteRenderer SPRenderer;
    public Sprite lvl1Sprite;
    public Sprite lvl2Sprite;

    //Building Variables
    [HideInInspector]
    public int maxLevel = 3;
    [HideInInspector]
    public int currentLevel;

    #endregion


    [SerializeField]
    private bool Damaged = false;

    #region Interaction
    //Icon Values
    public GameObject canvas;
    public GameObject HealthObj;
    public GameObject RepairObj;

    public Image HealthBar;
    #endregion

    private Resources resources;

    [Header("Locations and Placement")]
    [SerializeField]
    private LayerMask targetMaskLayer;
    private GroundTile tileBuildingOn;


    // Use this for initialization
    void Start () {
        if (currentLevel < 1)
        {
            currentLevel = 1;
        }


        SPRenderer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

        //Building Health
        HealthObj.SetActive(false);
        MaxHealth = Health;

        resources = GameObject.FindGameObjectWithTag(Tags.PLAYER_CONTROLLER_TAG).GetComponent<Resources>();

        resources.PeopleCap += HousingBonus;
        resources.UpdateInfo();


        FindTileOn();
        TurnOffDisplay();





    }

    private void FindTileOn()
    {
        //Used to determine if can build on this spot or not
        RaycastHit2D hit2d = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 100, targetMaskLayer);
        if (hit2d.collider.tag == Tags.GROUND_TAG)
        {

            tileBuildingOn = hit2d.collider.GetComponent<GroundTile>();
            if (tileBuildingOn != null)
            {
                tileBuildingOn.hasItem = true;
                tileBuildingOn.TileObj = gameObject;
            }
        }


    }

    private void TurnOffDisplay()
    {
        HealthObj.SetActive(false);
        RepairObj.SetActive(false);
        canvas.SetActive(false);

    }

    public void CalHealth()
    {


        if (Health <= 0)
        {
            BuildingDestroyed();
        }
        else if (Health < MaxHealth)
        {
            Damaged = true;
           // RepairObj.SetActive(true);
            HealthObj.SetActive(true);
            HealthBar.fillAmount = Health / 100f;

        }
        else if (Health == MaxHealth)
        {
            Damaged = false;
           // RepairObj.SetActive(false);
            HealthObj.SetActive(false);
            HealthBar.fillAmount = Health / 100f;

        }


    }

    public void Repair()
    {

    }

    public void Demolish()
    {
        resources.PeopleCap -= HousingBonus;
        resources.Wood += 15;
        resources.UpdateInfo();
    }

    public void Upgrade()
    {
        //Check if still Upgradable
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            //Set to sprite2
            if (currentLevel == 2)
            {
                SPRenderer.sprite = lvl2Sprite;
                HousingBonus += 3;
                resources.PeopleCap += 3;
               // resources.UpdateInfo();
            }
         
            
        }

    }

    public void BuildingDestroyed()
    {
        resources.PeopleCap -= HousingBonus;
        resources.UpdateInfo();
    }


    private void OnMouseOver()
    {
        canvas.SetActive(true);
    }

    private void OnMouseExit()
    {

        canvas.SetActive(false);

    }


}

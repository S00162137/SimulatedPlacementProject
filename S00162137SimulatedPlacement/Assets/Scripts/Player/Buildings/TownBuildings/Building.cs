using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour {



    private AudioSource sound;
    public AudioClip buildSound;



    [Header("Stats")]
    public float Health = 100;
    private float MaxHealth;
    public float BuildingDefence = 0;

    [HideInInspector]
    public BuildingStats buildingstats;
    [HideInInspector]
    public float damageTaken;

    #region Cost Variables

    public int refundWood;
    public int refundStone;
    public int refundPeople;

    #endregion

    [Header("Upgrade and Sprites")]
    #region Upgrade variables
    //Upgrade Variables


    public int upgradeWoodCost;
    public int upgradeStoneCost;
    public int upgradeMetalCost;

    public SpriteRenderer SPRenderer;
    public Sprite lvl1Sprite;
    public Sprite lvl2Sprite;
    public Sprite lvl3Sprite;

    //Building Variables
    [HideInInspector]
    public int maxLevel = 3;
    [HideInInspector]
    public int currentLevel;

    #endregion

    [Header("Locations and Placement")]
    [SerializeField]
    private LayerMask targetMaskLayer;
    private GroundTile tileBuildingOn;

    [Header("Ui Elements")]
    #region UiObjects

    Animator anim;


    //Icon Variables
    public GameObject UpgradeObj;
    public GameObject DemolishObj;
    public GameObject RepairObj;


    public GameObject canvas;
    public GameObject HealthObj;
    public Image HealthBar;

    [SerializeField]
    private bool Damaged = false;


    private bool CirclePopUp = false;
    #endregion

    //[HideInInspector]
    public Resources resources;
    private Text errorTextDisplay;

    // Use this for initialization
    void Start () {
        //UpgradeObj.SetActive(false);
        //DemolishObj.SetActive(false);
        //RepairObj.SetActive(false);
        if (currentLevel < 1)
        {
            currentLevel = 1;
        }

        FindTileOn();
        TurnOffDisplay();
        buildingstats = GetComponent<BuildingStats>();
        SPRenderer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();


        MaxHealth = Health;

        resources = GameObject.FindGameObjectWithTag(Tags.PLAYER_CONTROLLER_TAG).GetComponent<Resources>();

        canvas.SetActive(false);


        resources.UpdateInfo();




        sound = gameObject.GetComponent<AudioSource>();
        sound.volume = GameObject.Find(Tags.GAMECONTROLLER_TAG).GetComponent<OptionSettings>().SoundEffects;
        sound.PlayOneShot(buildSound);

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

    public void DoThing()
    {

    }

    public void CalHealth(float amount)
    {

        HealthObj.SetActive(true);

        amount -= BuildingDefence;
        Debug.Log(amount);

        Health = Health - amount;
        Debug.Log(Health);

        if (Health <= 0)
        {
            buildingstats.Alive = false;
        }

        //buildingstats.CurrentHealth(amount);

        if (Health < MaxHealth)
        {
            //For repair costs
            damageTaken += amount;

            Damaged = true;
            RepairObj.SetActive(true);
            HealthObj.SetActive(true);
            HealthBar.fillAmount = Health / MaxHealth;

            //destroyed building
            if (buildingstats.Alive == true)
            {

            }
            else // destroy building
            {
                BuildingDestroyed();
            }
        }

    }

    public void Repair()
    {
        if (resources.Wood >= damageTaken)
        {
            resources.Wood -= (int)damageTaken;
            Damaged = false;
            RepairObj.SetActive(false);
            HealthObj.SetActive(false);
            buildingstats.health = buildingstats.Maxhealth;
            HealthBar.fillAmount = buildingstats.health / buildingstats.Maxhealth;

        }
    }

    public void Demolish()
    {
        Workers w = GetComponent<Workers>();
        w.Unemployed();

        resources.Wood += refundWood;
        resources.Stone += refundStone;
        resources.People += refundPeople;
        tileBuildingOn.hasItem = false;
        //clear ground obj so can build
        tileBuildingOn.TileObj = null;
        tileBuildingOn.hasItem = false;

        Destroy(gameObject);
    }

    public void BuildingDestroyed()
    {
        tileBuildingOn.hasItem = false;
        tileBuildingOn.TileObj = null;


        
            //insert animations etc here
            if (GetComponent<Townhall>() != null)
            {
                Townhall tempEHALL = GetComponent<Townhall>();
                tempEHALL.TownHallDestroyed();
            }


        


        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {

        canvas.SetActive(true);
     
    }

    private void OnMouseExit()
    {

        canvas.SetActive(false);
 
    }

        //damage dection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.ENEMY_TAG)
        {
            EnemyStats estats = collision.gameObject.GetComponent<EnemyStats>();
            CalHealth(estats.Power);
            estats.KnockBacked(estats.KnockBackForce, transform.position);


        }
        if (collision.gameObject.tag == Tags.GIBBLER_TAG)
        {
            GibblerStats gstats = collision.gameObject.GetComponent<GibblerStats>();
            CalHealth(gstats.Power);
            gstats.Knockedback(gstats.KnockbackForce / 2, transform.position);

        }
    }

}

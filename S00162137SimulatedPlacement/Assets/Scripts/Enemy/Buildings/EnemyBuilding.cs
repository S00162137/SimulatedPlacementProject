using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBuilding : MonoBehaviour {

    //List varaibles
    [Header("BuildingType")]
    private EnemyBuildingManager EBuildingManager;
    public enum buildingType {Townhall, house, farm, lumbermill, mine, barracks,  gateway, tower };
    public buildingType buildtype;

    //Health Variables
    [Header("Stats")]
    [SerializeField]
    private float MaxHealth;
    public float currentHealth;
    public float damageTaken;
    public float BuildingDefence;

    [HideInInspector]
    public EnemyResources enemyresources;

    [Header("Resouce Costs")]
    public int WoodCost;
    public int StoneCost;
    public int MetalCost;
    public int VillagerCost;


    [Header("UI Elements")]
    [SerializeField]
    private GameObject CanvasObj;
    [SerializeField]
    private Image HealthBar;

    public bool UnderAttack = false;
    private float attackTimer = 0f;
    private float attackDuration = 15f;

	// Use this for initialization
	void Start () {


        EBuildingManager = GameObject.FindGameObjectWithTag(Tags.ENEMY_CONTROLLER_TAG).GetComponent<EnemyBuildingManager>();
        enemyresources = GameObject.FindGameObjectWithTag(Tags.ENEMY_CONTROLLER_TAG).GetComponent<EnemyResources>();

        currentHealth = MaxHealth;
        AssignToManagerBuildings();



	}


 
    private void FixedUpdate()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDuration)
        {
            UnderAttack = false;
        }
    }


    public void TakeDamage(float DamageAmount)
    {
        DamageAmount = DamageAmount - BuildingDefence;
        damageTaken += DamageAmount;
        currentHealth -= DamageAmount;

        CanvasObj.SetActive(true);
        HealthBar.fillAmount = currentHealth / MaxHealth;
        if (currentHealth <= 0)
        {
            EBuildingManager.DamagedBuildings.Remove(gameObject);
            BuildingDestroy();
        }


    }

    public void Repair()
    {
        enemyresources.Wood -= (int)damageTaken;
        currentHealth += damageTaken;
        damageTaken = 0;

    }

    public void BuildingDestroy()
    {
        //insert animations etc here
        if (GetComponent<EnemyTownhall>() != null)
        {
            EnemyTownhall tempEHALL = GetComponent<EnemyTownhall>();
            tempEHALL.TownHallDestroyed();
        }
        Destroy(gameObject);
    }

    public void AssignToManagerBuildings()
    {
        switch (buildtype)
        {
            case buildingType.Townhall:
                EBuildingManager.enemyTownhall = gameObject;
                EBuildingManager.BuildingsList.Add(gameObject);
                
                break;

            case buildingType.house:
                EBuildingManager.EnemyHouse.Add(gameObject);
                EBuildingManager.BuildingsList.Add(gameObject);
                EBuildingManager.NumberOfHouses++;
                break;

            case buildingType.farm:
                EBuildingManager.EnemyFarms.Add(gameObject);
                EBuildingManager.BuildingsList.Add(gameObject);
                EBuildingManager.NumberOFarms++;

                break;

            case buildingType.lumbermill:
                EBuildingManager.EnemyLumbermill.Add(gameObject);
                EBuildingManager.BuildingsList.Add(gameObject);
                EBuildingManager.NumberOfLumber++;

                break;

            case buildingType.mine:
                EBuildingManager.EnemyMine.Add(gameObject);
                EBuildingManager.BuildingsList.Add(gameObject);
                EBuildingManager.NumberOfMines++;

                break;

            case buildingType.barracks:
                EBuildingManager.EnemyBarracks.Add(gameObject);
                EBuildingManager.BuildingsList.Add(gameObject);
                EBuildingManager.NumberOfBarracks++;

                break;


            case buildingType.gateway:
                EBuildingManager.EnemyGateway.Add(gameObject);
                EBuildingManager.BuildingsList.Add(gameObject);
                break;

            case buildingType.tower:
                EBuildingManager.EnemyTowers.Add(gameObject);
                EBuildingManager.BuildingsList.Add(gameObject);
                break;

            default:
                break;
        }
    }

    public void DeductResources()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.PLAYERWEAPON_TAG)
        {
            WeaponScript wScript = collision.gameObject.GetComponent<WeaponScript>();
           
            TakeDamage(wScript.Power * 0.75f);

            //Asign Buildig under attack
            EBuildingManager.NeedToDefend = true;
            EBuildingManager.buildingUnderAttack = gameObject;
            EBuildingManager.DamagedBuildings.Add(gameObject);

            UnderAttack = true;
            attackTimer = 0f;



        }

        else if (collision.gameObject.tag == Tags.MILITIA_TAG)
        {

            MilitiaStats mStats = collision.gameObject.GetComponent<MilitiaStats>();
            mStats.KnockedBack(mStats.KnockBackForce, transform.position);
            TakeDamage(mStats.Power);

            //Asign Buildig under attack
            EBuildingManager.NeedToDefend = true;
            EBuildingManager.buildingUnderAttack = gameObject;
            EBuildingManager.DamagedBuildings.Add(gameObject);

            UnderAttack = true;
            attackTimer = 0f;
        }

        else if (collision.gameObject.tag == Tags.GIBBLER_TAG) 
        {
            GibblerStats gStats = collision.gameObject.GetComponent<GibblerStats>();
            gStats.Knockedback(gStats.KnockbackForce, transform.position );
            TakeDamage(gStats.Power);


            //Asign Buildig under attack
            EBuildingManager.NeedToDefend = true;
            EBuildingManager.buildingUnderAttack = gameObject;
            EBuildingManager.DamagedBuildings.Add(gameObject);

            UnderAttack = true;
            attackTimer = 0f;
        }

    }


}

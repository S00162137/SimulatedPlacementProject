using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanelManager : MonoBehaviour {

    //If true then cannot build
    public bool templateExists = false;
    //Build Panel
    public GameObject ConstructPanel;

    public Button btnHouse;
    public GameObject houseTemplate;


    public Button btnWoodCutter;
    public GameObject woodTemplate;


    public Button btnFarm;
    public GameObject farmTemplate;


    public Button btnMine;
    public GameObject mineTemplate;


    public Button btnBarracks;
    public GameObject BarracksTemplate;

    public Button btnGateway;
    public GameObject GatewayTemplate;

    public Button btnTower;
    public GameObject TowerTemplate;


    public enum BuildingType { House, Lumbermill,Mine, Farm, Barracks, Gateway, Tower};

    #region Info Display

    [Header("Info Display")]
    [SerializeField]
    private Sprite HouseImage;
    [SerializeField]
    private Sprite LumbermillImage;
    [SerializeField]
    private Sprite MineImage;
    [SerializeField]
    private Sprite FarmhouseImage;
    [SerializeField]
    private Sprite BarracksImage;
    [SerializeField]
    private Sprite TowerImage;
    [SerializeField]
    private Sprite GatewayImage;

    [Header("Info UI Elements")]
    public GameObject InfoDisplay;
    public Image BuildingDisplay;
    public Text WoodCostText;
    public Text StoneCostText;
    public Text MetalCostText;
    public Text VillagerCostText;


    #endregion


    public GameObject instansiatedBuilding;

	// Use this for initialization
	void Start () {

        string dir = "/Canvas/BottomBar/BuildingInfo";
        InfoDisplay = GameObject.Find(dir);
        BuildingDisplay = GameObject.Find("/Canvas/BottomBar/BuildingInfo/BuildingImage").GetComponent<Image>();
        WoodCostText = GameObject.Find(dir + "/WoodImage/WoodText").GetComponent<Text>();
        StoneCostText = GameObject.Find(dir + "/StoneImage/StoneText").GetComponent<Text>();
        MetalCostText = GameObject.Find(dir + "/MetalImage/MetalText").GetComponent<Text>();
        VillagerCostText = GameObject.Find(dir + "/PeopleImage/PeopleText").GetComponent<Text>();



        btnHouse = GameObject.Find(Tags.DirBuildPanelHouse).GetComponent<Button>();
        btnWoodCutter = GameObject.Find(Tags.DirBuildPanelLumbermill).GetComponent<Button>();
        btnFarm = GameObject.Find(Tags.DirBuildPanelFarm).GetComponent<Button>();
        btnMine = GameObject.Find(Tags.DirBuildPanelMine).GetComponent<Button>();
        btnBarracks = GameObject.Find(Tags.DirBuildPanelBarracks).GetComponent<Button>();
        btnGateway = GameObject.Find(Tags.DirBuildPanelGateway).GetComponent<Button>();
        btnTower = GameObject.Find(Tags.DirBuildPanelTower).GetComponent<Button>();
        InfoDisplay = GameObject.Find(Tags.DirBuildDisplay);

        //Sets Bttn Command
        btnHouse.onClick.AddListener(delegate { SpawnTemplateBuilding(BuildingType.House); });
        btnFarm.onClick.AddListener(delegate { SpawnTemplateBuilding(BuildingType.Farm); });
        btnMine.onClick.AddListener(delegate { SpawnTemplateBuilding(BuildingType.Mine); });
        btnWoodCutter.onClick.AddListener(delegate { SpawnTemplateBuilding(BuildingType.Lumbermill); });
        btnBarracks.onClick.AddListener(delegate { SpawnTemplateBuilding(BuildingType.Barracks); });
        btnGateway.onClick.AddListener(delegate { SpawnTemplateBuilding(BuildingType.Gateway); });
        btnTower.onClick.AddListener(delegate { SpawnTemplateBuilding(BuildingType.Tower); });



        InfoDisplay.SetActive(false);


    }

 
    #region Instansiate Templates
    //Load Template buildings for placing
   
    public void SpawnTemplateBuilding(BuildingType Building)
    {
        if (templateExists == true)
        {
            if (GameObject.FindGameObjectsWithTag(Tags.TEMPLATEBUILDING_TAG).Length >= 1)
            {
                foreach (GameObject item in GameObject.FindGameObjectsWithTag(Tags.TEMPLATEBUILDING_TAG))
                {
                    Destroy(item);
                }
            }
            templateExists = false;
        }


        switch (Building)
        {

            case BuildingType.House:
                Instantiate(houseTemplate, new Vector3(0, 0, 1), gameObject.transform.rotation);
                instansiatedBuilding = GatewayTemplate;
                templateExists = true;
                break;

            case BuildingType.Lumbermill:
                Instantiate(woodTemplate, new Vector3(0, 0, 1), gameObject.transform.rotation);
                instansiatedBuilding = GatewayTemplate;
                templateExists = true;
                break;

            case BuildingType.Mine:
                Instantiate(mineTemplate, new Vector3(0, 0, 1), gameObject.transform.rotation);
                instansiatedBuilding = GatewayTemplate;
                templateExists = true;
                break;

            case BuildingType.Farm:
                Instantiate(farmTemplate, new Vector3(0, 0, 1), gameObject.transform.rotation);
                instansiatedBuilding = GatewayTemplate;
                templateExists = true;
                break;

            case BuildingType.Barracks:
                Instantiate(BarracksTemplate, new Vector3(0, 0, 1), gameObject.transform.rotation);
                instansiatedBuilding = GatewayTemplate;
                templateExists = true;
                break;

            case BuildingType.Gateway:
                Instantiate(GatewayTemplate, new Vector3(0, 0, 1), gameObject.transform.rotation);
                instansiatedBuilding = GatewayTemplate;
                templateExists = true;
                break;

            case BuildingType.Tower:
                Instantiate(TowerTemplate, new Vector3(0, 0, 1), gameObject.transform.rotation);
                instansiatedBuilding = GatewayTemplate;
                templateExists = true;
                break;








        }
    }


    public void DisplayRequirments(int buttonHover)
    {
        InfoDisplay.SetActive(true);
        // BuildingDisplay.sprite = 

        switch (buttonHover)
        {

            //House
            case 0:
                InfoDisplay.SetActive(true);
                BuildingDisplay.sprite = HouseImage;
                WoodCostText.text = "50";
                StoneCostText.text = "0";
                MetalCostText.text = "0";
                VillagerCostText.text = "0";

                break;

                //LumberMill
            case 1:
                InfoDisplay.SetActive(true);
                BuildingDisplay.sprite = LumbermillImage;
                WoodCostText.text = "50";
                StoneCostText.text = "0";
                MetalCostText.text = "0";
                VillagerCostText.text = "3";


                break;

            //Mine
            case 2:
                InfoDisplay.SetActive(true);
                BuildingDisplay.sprite = MineImage;
                WoodCostText.text = "60";
                StoneCostText.text = "3";
                MetalCostText.text = "0";
                VillagerCostText.text = "3";


                break;

                //Farm
            case 3:
                InfoDisplay.SetActive(true);
                BuildingDisplay.sprite = FarmhouseImage;
                WoodCostText.text = "50";
                StoneCostText.text = "0";
                MetalCostText.text = "0";
                VillagerCostText.text = "3";


                break;

                //Barracks
            case 4:
                InfoDisplay.SetActive(true);
                BuildingDisplay.sprite = BarracksImage;
                WoodCostText.text = "60";
                StoneCostText.text = "5";
                MetalCostText.text = "0";
                VillagerCostText.text = "0";


                break;

                //Tower
            case 5:
                InfoDisplay.SetActive(true);
                BuildingDisplay.sprite = TowerImage;
                WoodCostText.text = "70";
                StoneCostText.text = "5";
                MetalCostText.text = "1";
                VillagerCostText.text = "1";


                break;

            //Gateway

            case 6:
                InfoDisplay.SetActive(true);
                BuildingDisplay.sprite = GatewayImage;
                WoodCostText.text = "80";
                StoneCostText.text = "5";
                MetalCostText.text = "2";
                VillagerCostText.text = "0";


                break;

            case 7:
                InfoDisplay.SetActive(true);
                BuildingDisplay.sprite = TowerImage;
                WoodCostText.text =  "70";
                StoneCostText.text = "5";
                MetalCostText.text = "1";
                VillagerCostText.text = "1";


                break;






        }

    }

    public void DisableInfoPanel()
    {
        InfoDisplay.SetActive(false);
    }
    #endregion

}

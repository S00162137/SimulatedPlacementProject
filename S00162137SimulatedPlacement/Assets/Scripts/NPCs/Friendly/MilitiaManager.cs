using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilitiaManager : MonoBehaviour {

    [Header("Units")]
    public List<GameObject> Militia = new List<GameObject>();
    public List<GameObject> SoldiersList = new List<GameObject>();
    public List<GameObject> PikemenList = new List<GameObject>();
    public List<GameObject> ShieldBearerList = new List<GameObject>();
    public List<GameObject> ArcherList = new List<GameObject>();

    [Header("UI Elements")]
    #region UiElements

    public GameObject SoldierButtons;
    public GameObject ShieldbearerButtons;
    public GameObject PikemanButtons;
    public GameObject AllUnitsButtons;

    #endregion



    // Use this for initialization
    void Start () {


        SoldierButtons = GameObject.Find(Tags.DirSoldierBTNS);
        ShieldbearerButtons = GameObject.Find(Tags.DirShieldbearerBTNS);
        PikemanButtons = GameObject.Find(Tags.DirPikemenBTNS);
        AllUnitsButtons = GameObject.Find(Tags.DirMlitaryBTNS);


        SoldierButtons.SetActive(false);
        ShieldbearerButtons.SetActive(false);
        PikemanButtons.SetActive(false);
        AllUnitsButtons.SetActive(false);

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (GameObject unit in Militia)
            {
                Soldier temp = unit.GetComponent<Soldier>();
                temp.Advancing = !temp.Advancing;
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (GameObject unit in Militia)
            {
                Soldier temp = unit.GetComponent<Soldier>();
                temp.FollowCommand();
            }
        }
	}


    public void AdvanceAll()
    {
        foreach (GameObject unit in Militia)
        {
            Soldier temp = unit.GetComponent<Soldier>();
            temp.Advancing = !temp.Advancing;
        }
    }

    public void AdvanceSoldier()
    {

        foreach (GameObject unit in SoldiersList)
        {
            Soldier temp = unit.GetComponent<Soldier>();
            if (temp.unittype == Soldier.UnitType.Soldier)
            {
                temp.Advancing = !temp.Advancing;
            }

        }
    }

    public void AdvancePikemen()
    {
        foreach (GameObject unit in PikemenList)
        {
            Soldier temp = unit.GetComponent<Soldier>();
            if (temp.unittype == Soldier.UnitType.Pikeman)
            {
                temp.Advancing = !temp.Advancing;
            }

        }
    }

    public void AdvanceShieldbearers()
    {
        foreach (GameObject unit in ShieldBearerList)
        {
            Soldier temp = unit.GetComponent<Soldier>();
            if (temp.unittype == Soldier.UnitType.Shieldbearer)
            {
                temp.Advancing = !temp.Advancing;
            }
        }
    }

    public void FollowSpecific(int UnitNumber)
    {
        switch (UnitNumber)
        {

            case 0:
                foreach (GameObject Unit in SoldiersList)
                {
                     Unit.GetComponent<Soldier>().FollowCommand();
                }
            break;

            case 1:
                foreach (GameObject Unit in ShieldBearerList)
                {
                    Unit.GetComponent<Soldier>().FollowCommand();
                }
                break;

            case 2:
                foreach (GameObject Unit in PikemenList)
                {
                    Unit.GetComponent<Soldier>().FollowCommand();
                }
                break;

            case 3:
                foreach (GameObject Unit in Militia)
                {
                    Unit.GetComponent<Soldier>().FollowCommand();
                }
                break;


            default:
                break;
        }
    }

    public void EnableSoldierPanel(int panelNumber)
    {
        switch (panelNumber)
        {
            case 0:

                SoldierButtons.SetActive(true);
                ShieldbearerButtons.SetActive(false);
                PikemanButtons.SetActive(false);
                AllUnitsButtons.SetActive(false);

                break;

            case 1:
                SoldierButtons.SetActive(false);
                ShieldbearerButtons.SetActive(true);
                PikemanButtons.SetActive(false);
                AllUnitsButtons.SetActive(false);

                break;

            case 2:
                SoldierButtons.SetActive(false);
                ShieldbearerButtons.SetActive(false);
                PikemanButtons.SetActive(true);
                AllUnitsButtons.SetActive(false);

                break;


            case 3:
                SoldierButtons.SetActive(false);
                ShieldbearerButtons.SetActive(false);
                PikemanButtons.SetActive(false);
                AllUnitsButtons.SetActive(true);

                break;


        }
    }

    public void DisableSoldierPanel()
    {

        SoldierButtons.SetActive(false);
        ShieldbearerButtons.SetActive(false);
        PikemanButtons.SetActive(false);
        AllUnitsButtons.SetActive(false);
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour {
    
    [Header("Starting Resources")]
    public int Food = 50;
    public int Wood = 50;
    public int Stone = 10;
    public int Metal = 0;
    public int People=0; // total Pop

    public int Employed;
    public int Unemployed;

    [Header("Starting Caps")]
    public int FoodCap = 100;
    public int WoodCap = 80;
    public int StoneCap = 50;
    public int MetalCap = 25;
    public int PeopleCap = 7;


    [Header("Resource Deduction")]
    public int peopleFoodCost;
    public int peopleWoodCost;


    private float timer = 0f;
    [SerializeField]
    private float defaultTimer = 20f;


    public Text foodText;
    public Text woodText;
    public Text stoneText;
    public Text metalText;
    public Text peopleText;
    public Text unemployedPeopleText;


    private void Awake()
    {
        foodText = GameObject.Find(Tags.DirFoodText).GetComponent<Text>();
        woodText = GameObject.Find(Tags.DirWoodText).GetComponent<Text>();
        stoneText = GameObject.Find(Tags.DirStoneText).GetComponent<Text>();
        metalText = GameObject.Find(Tags.DirMetalText).GetComponent<Text>();
        peopleText = GameObject.Find(Tags.DirPeopleText).GetComponent<Text>();
        unemployedPeopleText = GameObject.Find(Tags.DirUnemployedPeopleText).GetComponent<Text>();
        UpdateInfo();

    }
    void Start ()
    {
    }


    

    private void EndTimer()
    {
        Food -= peopleFoodCost * People;
        Wood -= peopleWoodCost * People;
        UpdateInfo();
    }

    public void CalResources()
    {

    }

    public void LowOnResource()
    {

    }

    public void UpdateInfo()
    {
        foodText.text = Food.ToString() + " / " + FoodCap.ToString();

        woodText.text = Wood.ToString() + " / " + WoodCap.ToString();

        stoneText.text = Stone.ToString() + " / " + StoneCap.ToString();

        metalText.text = Metal.ToString() + " / " + MetalCap.ToString();

        peopleText.text = People.ToString() + " / " + PeopleCap.ToString();

        unemployedPeopleText.text = Unemployed.ToString() + " / " + PeopleCap.ToString();


    }


}

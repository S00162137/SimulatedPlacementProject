using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Townhall : Building {

    public GameObject PlayerPrefab;
    public WinCondition winCondition;


    [Header("Units")]
    //For Spawning Villagers
    private int spawnQ;
    public int maxSpawnQ;
    public Transform spawnPoint;
    public Image vilagerIcon;
    public Vector3 iconStackPoint;

    public GameObject villager;
    private int VillagerStart = 5;




    public float defaultTimer = 10f;
    private float timer;
    private bool MakeVillager = false;
    [SerializeField]
    private bool instartMenu = false;

    [Header("UI Elements")]
    [SerializeField]
    private GameObject canvasObj;
    [SerializeField]
    private Image progressBar;

    // Use this for initialization
    void Awake () {
        Health = 500;

        canvasObj.SetActive(false);

        /*
        resources = GameObject.FindGameObjectWithTag(Tags.SCENECONTROLER_TAG).GetComponent<Resources>();
        if (instartMenu == false)
        {

            for (int i = 0; i < VillagerStart; i++)
            {
                Vector3 spawnPosition = new Vector3(transform.position.x, spawnPoint.position.y, -2f);
                Instantiate(villager, spawnPosition, spawnPoint.rotation);
            }
        }
        */
        Instantiate(PlayerPrefab, new Vector3(transform.position.x, transform.position.y, -1f), transform.rotation);
        PlayerStats tempStats = PlayerPrefab.GetComponent<PlayerStats>();
        tempStats.townHall = gameObject;



        for (int i = 0; i <= VillagerStart; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, 0.05f, -2f);
            Instantiate(villager, spawnPosition, spawnPoint.rotation);
        }

        //Used for when Building is destroyed
        winCondition = GameObject.FindGameObjectWithTag(Tags.SCENECONTROLER_TAG).GetComponent<WinCondition>();
    }


  

    // Update is called once per frame
    void Update () {

        if (spawnQ > 0)
        {
            canvasObj.SetActive(true);

                timer += Time.deltaTime;
                if (timer >= defaultTimer)
                {
                timer = 0;
                EndTimer();
                Debug.Log(timer);
                }
                progressBar.fillAmount = timer / defaultTimer;

        }
    }

    void EndTimer()
    {
        Vector2 spawnPosition = new Vector2(transform.position.x, 0.05f);
        Instantiate(villager, spawnPosition, spawnPoint.rotation);
        villager.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, -2f);
        spawnQ--;


        
    }

    public void QueueVillager()
    {
        if (resources.Food >= 20 && resources.People <= resources.PeopleCap)
        {
            if (spawnQ != maxSpawnQ)
            {
                Debug.Log("Villager added to queue");
                resources.Food -= 20;
                resources.UpdateInfo();

                spawnQ++;
                //Instantiate();
                //QDisplay.Add();
            }
        }
    }

    public void UpgradeTownHall()
    {
        Debug.Log("Upgraded");
    }


    public void TownHallDestroyed()
    {
        winCondition.DetermineWin(1);

    }


}

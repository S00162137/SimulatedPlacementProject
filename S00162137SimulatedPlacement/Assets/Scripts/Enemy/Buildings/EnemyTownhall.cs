using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTownhall : MonoBehaviour {


    public WinCondition winCondition;

    [SerializeField]
    private GameObject EnemyPlayerObj;

    [SerializeField]
    private GameObject EVillager;
    public Transform spawnPoint;

    private int VillagerFoodCost;

    private EnemyResources eresources;
    private float timer = 0f;
    private float defaultTimer = 20f;

    [SerializeField]
    private int StartingVillagerCount;

    [SerializeField]
    private int VillagersToSpawn = 0;

    public Vector2 TownHallPosition;
    //Need to make it so AI Player goes to townhall, and does method

    void Start () {


        eresources = GameObject.FindGameObjectWithTag(Tags.ENEMY_CONTROLLER_TAG).GetComponent<EnemyResources>();

        TownHallPosition = gameObject.transform.position;

        //Make the enemy PLAYER
        Instantiate(EnemyPlayerObj, new Vector3(transform.position.x, EnemyPlayerObj.transform.position.y, -2f), EnemyPlayerObj.transform.rotation);


        winCondition = GameObject.FindGameObjectWithTag(Tags.SCENECONTROLER_TAG).GetComponent<WinCondition>();

        for (int i = 0; i < StartingVillagerCount; i++)
        {
            MakeNewVillager();
        }
    }

    void Update () {



        if (VillagersToSpawn >=1 && eresources.Food >= VillagerFoodCost)
        {
            timer += Time.deltaTime;
            if (timer >= defaultTimer)
            {

                if (VillagersToSpawn != 0)
                {
                    timer = 0;
                    EndTimer(); // when timer hits time, make villager
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            MakeNewVillager();
        }

	}

    public void MakeNewVillager()
    {
        Debug.Log("added");
        //Check if enough food, and enough rooms
        if (eresources.Food >= VillagerFoodCost && eresources.PeopleCap >= eresources.People)
        {
            Instantiate(EVillager, spawnPoint.position, EVillager.transform.rotation);
            eresources.Food -= VillagerFoodCost;
            EnemyResources eResources = GameObject.FindGameObjectWithTag(Tags.ENEMY_CONTROLLER_TAG).GetComponent<EnemyResources>();

            eResources.People++;

        }


    }


    private void EndTimer()
    {
        Instantiate(EVillager, gameObject.transform.position, EVillager.transform.rotation);
        eresources.Food -= VillagerFoodCost;
    }


    public void TownHallDestroyed()
    {
        winCondition.DetermineWin(2);

    }


}

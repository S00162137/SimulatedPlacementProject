using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workers : MonoBehaviour {



    public int workerCount = 3;
    public List<GameObject> workersList = new List<GameObject>();

    public Vector2 positionBuilding;
    public Vector2 positionWorkingTarget;
    public Vector2 PositionLeft;
    public Vector2 PositionRight;

	// Use this for initialization
	void Start () {

        positionBuilding = gameObject.transform.position;
        PositionLeft = new Vector2(positionBuilding.x - 5f, positionBuilding.y);
        PositionRight = new Vector2(positionBuilding.x + 5f, positionBuilding.y);


        //Find workers
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(Tags.VILLAGER_TAG))
        {
            if (workersList.Count < workerCount)
            {
                Villager villager = item.GetComponent<Villager>();
                if (villager.working == false)
                {
                    workersList.Add(item);
                    villager.AssignToBuilding(gameObject);

                }
            }
        }

	}


    public void Unemployed()
    {

        foreach (GameObject worker in workersList)
        {
            Villager villager = worker.GetComponent<Villager>();
          
                villager.UnEmployed();

            
        }

    }


	
}

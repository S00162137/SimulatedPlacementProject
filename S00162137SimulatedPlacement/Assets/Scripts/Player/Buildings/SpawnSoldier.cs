using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSoldier : MonoBehaviour {

    private Resources resource;

    public int spawnQ;
    public int maxSpawnQ;

    public Transform spawnPoint;
    public GameObject soldier;

    public float defaultTimer = 10f;
    [SerializeField]
    private float timer;

	// Use this for initialization
	void Start () {
        timer = defaultTimer;
        resource = GameObject.FindGameObjectWithTag(Tags.SCENECONTROLER_TAG).GetComponent<Resources>();
	}
	
	// Update is called once per frame
	void Update ()
    {


        if (spawnQ > 0 && resource.People >0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndTimer();
            }
        }




	}

    void EndTimer()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x , 0.05f, 0);
        Instantiate(soldier, spawnPosition, spawnPoint.rotation);
        spawnQ--;
        resource.People--;
        if (spawnQ >0)
        {
            timer = defaultTimer;
        }
    }

    public void AddToQueue()
    {

        if (resource.People >0)
        {
            if (spawnQ != maxSpawnQ)
            {
                spawnQ++;
            }
        }

     
    }


}

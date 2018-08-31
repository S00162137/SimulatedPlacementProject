using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLumbermill : MonoBehaviour {

    private EnemyResources eresources;

    private float timer = 0f;
    public float defaultTimer = 20f;


    public int woodGenAmount;
    private int woodCapIncreaseAmount = 5;

	void Start () {
        eresources = GameObject.FindGameObjectWithTag(Tags.ENEMY_CONTROLLER_TAG).GetComponent<EnemyResources>();
        eresources.WoodCap += woodCapIncreaseAmount;
	}
	
	void Update () {


        timer += Time.deltaTime;

        if (timer >= defaultTimer)
        {
            timer = 0;
            EndTimer();
        }

	}



    public void EndTimer()
    {
        eresources.Wood += woodGenAmount;
    }



}

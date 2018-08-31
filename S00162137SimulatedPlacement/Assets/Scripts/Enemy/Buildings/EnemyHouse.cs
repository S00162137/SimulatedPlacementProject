using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHouse : MonoBehaviour {

    private EnemyBuilding thisBuilding;

    private int peopleHouseing = 5;

	// Use this for initialization
	void Start () {

        thisBuilding = GetComponent<EnemyBuilding>();
        thisBuilding.enemyresources.PeopleCap += peopleHouseing;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

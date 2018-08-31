using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateControllers : MonoBehaviour {

    public GameObject PlayerController;
    public GameObject EnemyController;




	// Use this for initialization
	void Awake () {
        Instantiate(PlayerController, transform.position, transform.rotation);
        Instantiate(EnemyController, transform.position, transform.rotation);

    }


}

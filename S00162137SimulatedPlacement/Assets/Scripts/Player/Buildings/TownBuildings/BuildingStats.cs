using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStats : MonoBehaviour {



    public float health;
    public float Maxhealth =50;
    public float buildingDefence;

    public bool Alive = true;
	// Use this for initialization
	void Start () {
        health = Maxhealth;
	}
	

    public void CurrentHealth(float healthChangeAmount)
    {
        healthChangeAmount -= buildingDefence;
        health = health - healthChangeAmount;

        if (health <= 0)
        {
            Alive = false;
        }
    }


    public void DestroyBuilding()
    {
        //insert animations etc here
        if (GetComponent<Townhall>() != null)
        {
            Townhall tempEHALL = GetComponent<Townhall>();
            tempEHALL.TownHallDestroyed();
        }
        Destroy(gameObject);


    }



}

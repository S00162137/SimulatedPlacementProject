using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDrop : MonoBehaviour {


    public int ValueAmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.PLAYER_TAG || collision.gameObject.tag == Tags.VILLAGER_TAG)
        {
            Resources resource = GameObject.FindGameObjectWithTag(Tags.PLAYER_CONTROLLER_TAG).GetComponent<Resources>();

            if (resource.Food <= resource.FoodCap - ValueAmount)
            {
                resource.Food += ValueAmount;
                resource.UpdateInfo();
                Destroy(gameObject);
            }

        }


        else if (collision.gameObject.tag == Tags.ENEMY_PLAYER || collision.gameObject.tag == Tags.ENEMY_TAG)
        {

            EnemyResources resource = GameObject.FindGameObjectWithTag(Tags.ENEMY_CONTROLLER_TAG).GetComponent<EnemyResources>();

            if (resource.Wood <= resource.FoodCap - ValueAmount)
            {

                resource.Food += ValueAmount;
                Destroy(gameObject);
            }
        }
    }


}

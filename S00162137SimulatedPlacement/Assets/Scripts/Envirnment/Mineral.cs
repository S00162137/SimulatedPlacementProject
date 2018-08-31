using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour {


    public int valueAmount = 1;
    [SerializeField]
    private bool Stone = true;
 

    private void OnCollisionEnter2D(Collision2D collision)
    {
            //Touches Player
            if (collision.gameObject.tag == Tags.PLAYER_TAG || collision.gameObject.tag == Tags.VILLAGER_TAG)
            {
                Resources resource = GameObject.FindGameObjectWithTag(Tags.PLAYER_CONTROLLER_TAG).GetComponent<Resources>();

            //Stone
                if (Stone == true)
                {
                    if (resource.Stone <= resource.StoneCap - valueAmount)
                    {
                        resource.Stone += valueAmount;
                        resource.UpdateInfo();
                        Destroy(gameObject);
                    }
                }

                //Metal
                else
                {
                    if (resource.Metal <= resource.MetalCap - valueAmount)
                    {
                        resource.Metal += valueAmount;
                        resource.UpdateInfo();

                         Destroy(gameObject);
                    }
                }

        


            }

        else if (collision.gameObject.tag == Tags.ENEMY_PLAYER || collision.gameObject.tag == Tags.ENEMY_TAG)
        {

            EnemyResources resource = GameObject.FindGameObjectWithTag(Tags.ENEMY_CONTROLLER_TAG).GetComponent<EnemyResources>();



            //Stone
            if (Stone == true)
            {
                if (resource.Stone <= resource.StoneCap - valueAmount)
                {

                    resource.Stone += valueAmount;
                    Destroy(gameObject);
                }
            }

            //Metal
            else
            {
                if (resource.Metal <= resource.MetalCap - valueAmount)
                {

                    resource.Metal += valueAmount;

                    Destroy(gameObject);
                }
            }
        }

     

        
    }


}

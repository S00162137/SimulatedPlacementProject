using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceTower : MonoBehaviour {

    public List<GameObject> Enemies = new List<GameObject>();
    private GameObject targetEnemy;
    private float targetFirePointOff;
    private Vector3 targetPoint;


    public bool EnemiesInRange;
    public bool HasFired;

    private float rechargeTimer = 0f;
    public float FireRate = 3f;


    [SerializeField]
    private GameObject ArrowObj;
    [SerializeField]
    private Transform ArrowPoint;

	void Start () {
    
        

	}

    private void FixedUpdate()
    {
        if (EnemiesInRange == true)
        {
            //recharge
            if (HasFired == true)
            {
                rechargeTimer += Time.deltaTime;
                if (rechargeTimer >= FireRate)
                {
                    rechargeTimer = 0;
                    HasFired = false;
                }
            }
            //get target
            else
            {
                //Determine target
                GameObject tempTarget = Enemies[0];
                foreach (GameObject item in Enemies)
                {

                    if (Vector2.Distance(tempTarget.transform.position,transform.position) > Vector2.Distance(transform.position, item.transform.position))
                    {
                        targetEnemy = item;


                        Vector3 targ = targetEnemy.transform.position;
                        targ.z = 0f;

                        Vector3 objectPos = transform.position;
                        targ.x = targ.x - objectPos.x;
                        targ.y = targ.y - objectPos.y;

                        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                        ArrowPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    }
                }

                Fire(targetEnemy);
            }

        }




    }

    private  void Fire(GameObject target )
    {

        HasFired = true;
        Instantiate(ArrowObj, ArrowPoint.transform);
        ArrowObj.GetComponent<Arrow>().targetPos = target.transform.position;
        ArrowObj.transform.LookAt(target.transform.position);
    }

  

 

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
            //checks if the item entering is relevate
            if (collision.gameObject.tag == Tags.GIBBLER_TAG)
            {
                Enemies.Add(collision.gameObject);
                EnemiesInRange = true;
            }
        


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        //checks if the item entering is relevate
        if (collision.gameObject.tag == Tags.GIBBLER_TAG)
        {
            Enemies.Remove(collision.gameObject);
            if (Enemies.Count <= 0)
            {
                EnemiesInRange = false;
            }
        }
    }
}

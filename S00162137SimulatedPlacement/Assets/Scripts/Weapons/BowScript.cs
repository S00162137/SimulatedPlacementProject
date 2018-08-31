using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour {

    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private Transform firePoint;
    private Vector3 fireDirection;

    private float timer = 0f;
    public float AttackCoolDownTimer = 1.5f;
    private bool canFire = true;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (canFire == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FireArrow();
                timer = 0f;
                canFire = false;
            }
        }


        timer += Time.deltaTime;
        if (timer >= AttackCoolDownTimer)
        {
            canFire = true;
        }
	}


    void FireArrow()
    {
        Instantiate(arrow, firePoint.position,transform.rotation);
        Arrow aScript = arrow.GetComponent<Arrow>();

    }


}

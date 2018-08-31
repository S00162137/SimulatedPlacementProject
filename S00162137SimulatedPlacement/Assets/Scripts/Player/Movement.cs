using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {


    //OtherObjects
    public GameObject backArm;



    public Rigidbody2D playerbody;
    //Bools
    [HideInInspector]
    public bool Attacking;
    [HideInInspector]
    public bool Moving;
    [HideInInspector]
    public bool jumping;
    [HideInInspector]
    public bool FacingRight =true;

    //Default Values
    private Vector3 leftScale = new Vector3(-2f,2,1);
    private Vector3 rightScale = new Vector3(2f, 2, 1);
    private Vector2 moveDir;

    private PlayerStats pStats;
  
    // Use this for initialization
    void Start () {
        playerbody = GetComponent<Rigidbody2D>();
        pStats = GetComponent<PlayerStats>();

	}
	
	// Update is called once per frame
	void Update () {

        if (pStats.IsAlive == true)
        {
            if (Input.GetKey(KeyCode.A))
            {

                gameObject.transform.localScale = leftScale;
                gameObject.transform.Translate(new Vector3(-0.1f, 0, 0) * pStats.moveSpeed);
                Moving = true;
                FacingRight = false;
            }


            else if (Input.GetKey(KeyCode.D))
            {

                gameObject.transform.localScale = rightScale;
                gameObject.transform.Translate(new Vector3(0.1f, 0, 0) * pStats.moveSpeed);
                Moving = true;
                FacingRight = true;

            }


            if (Moving == true)
            {

                //Moving Animation
                if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
                {
                    Moving = false;

                    //Idle Animation
                }
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (jumping == false)
                {
                    playerbody.AddForce(Vector2.up * pStats.jumpPower, ForceMode2D.Impulse);
                    jumping = true;
                }

            }


        }


    }
    private void FixedUpdate()
    {
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (jumping == true)
        {
            if (collision.gameObject.tag == Tags.GROUND_TAG)
            {
                jumping = false;
            }
        }
    }



}

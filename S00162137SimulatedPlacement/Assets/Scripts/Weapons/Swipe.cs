using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {

    //Other Objects
    public GameObject backArm;

    AudioClip swingSound;
    AudioSource Asource;

    //Player related variables
    private Movement playerMovement;
    public Vector3 rotateStartRight = new Vector3(0f,0f,80f);
    public Vector3 rotateStartLeft = new Vector3(0f, 0f, -80f);
    [SerializeField]
    private PlayerStats pStats;


    public float AttackingSpeed=300;
    private bool Attacking = false;

    //weapon related variables
    private int weaponSelectedValue = 0;


    public GameObject axeWeapon;


    public GameObject pickaxeWeapon;


    public GameObject swordWeapon;


    public GameObject bowWeapon;

   
    //Bow Related Variables
    public bool bowEquipped = false;


    private float MaxRot = 100f;
    private float MinRot = 0f;
    private Vector2 mousePos;



	// Use this for initialization
	void Start () {
        //backArm.SetActive(false);


        playerMovement = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<Movement>();

        pStats = gameObject.GetComponent<PlayerStats>();

        //Set Weapons
        pStats.equippedWeapon = PlayerStats.WeaponType.Axe;
        SwapWeapon(weaponSelectedValue);
        bowEquipped = false;



	}
	
	// Update is called once per frame
	void Update () {

        if (pStats.IsAlive == true)
        {
            //Manage attack bow or swipe
            if (bowEquipped == false)
            {
                // On leftButton click, and not already atacking
                if (Input.GetMouseButton(0) && Attacking == false)
                {
                    backArm.SetActive(true);
                    Attacking = true;
                    if (playerMovement.FacingRight == true)
                    {
                        backArm.transform.eulerAngles = rotateStartRight; // Set start angle
                    }
                    else
                    {
                        backArm.transform.eulerAngles = rotateStartLeft; // Set start angle
                    }

                }
            }

            //Fire with Bow
            if (bowEquipped == true)
            {

                if (playerMovement.FacingRight == false)
                {
                    backArm.transform.localScale = new Vector3(-1, 1, 1);
                }

                else
                {
                    backArm.transform.localScale = new Vector3(1, 1, 1);
                }
                Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 dir = Input.mousePosition - pos;


                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                backArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                ////adjust so can't exceed max or min
                //if (backArm.transform.rotation.z >MaxRot)
                //{
                //    backArm.transform.rotation.z  = MaxRot;
                //}
            }

            //If swipe attack
            if (Attacking == true)
            {

                backArm.transform.Rotate(Vector3.back * Time.deltaTime * AttackingSpeed);

                if (playerMovement.FacingRight == true)
                {
                    if (backArm.transform.rotation.z < 0)
                    {
                        Attacking = false;
                        backArm.SetActive(false);
                    }
                }



                else
                {
                    if (backArm.transform.rotation.z > 0)
                    {
                        Attacking = false;
                        backArm.SetActive(false);
                    }
                }
            }

            #region Weapon Select

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (weaponSelectedValue >= 0 && weaponSelectedValue < 2)
                {
                    weaponSelectedValue++;
                    SwapWeapon(weaponSelectedValue);
                }


            }

            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (weaponSelectedValue <= 2 && weaponSelectedValue > 0)
                {
                    weaponSelectedValue--;
                    SwapWeapon(weaponSelectedValue);
                }
            }


            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                weaponSelectedValue = 0;
                SwapWeapon(weaponSelectedValue);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                weaponSelectedValue = 1;
                SwapWeapon(weaponSelectedValue);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                weaponSelectedValue = 2;
                SwapWeapon(weaponSelectedValue);
            }

            /*
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                weaponSelectedValue = 3;
                SwapWeapon(weaponSelectedValue);
            }

    */
            #endregion

        }



    }




    public void SwapWeapon(int weaponValue)
    {


        switch (weaponValue)
        {

            //Equip Axe
            case 0:
                backArm.SetActive(true);

                bowEquipped = false;
                axeWeapon.SetActive(true);
                pickaxeWeapon.SetActive(false);
                swordWeapon.SetActive(false);
                bowWeapon.SetActive(false);
                backArm.SetActive(false);

           
                break;

                //Equip Pickaxe
            case 1:
                backArm.SetActive(true);

                bowEquipped = false;
                axeWeapon.SetActive(false);
                pickaxeWeapon.SetActive(true);
                swordWeapon.SetActive(false);
                bowWeapon.SetActive(false);

                backArm.SetActive(false);



                break;

                //equip Sword
            case 2:

                backArm.SetActive(true);
                bowEquipped = false;
                axeWeapon.SetActive(false);
                pickaxeWeapon.SetActive(false);
                swordWeapon.SetActive(true);
                bowWeapon.SetActive(false);
                backArm.SetActive(false);



                break;

                //Equip Bow
            case 3:
                backArm.SetActive(true);
                axeWeapon.SetActive(false);
                pickaxeWeapon.SetActive(false);
                swordWeapon.SetActive(false);
                bowWeapon.SetActive(true);
                bowEquipped = true;

                break;

         
    
        }
    }
 



}

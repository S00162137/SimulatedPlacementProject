using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FoodObject : MonoBehaviour {


    private float progressAmount = 0; // Player's progress on hitting
    private float progressmax = 80f;
    public float remainingAmount = 10;
    public float foodAmountCap = 10;

    public GameObject FoodItem;
    public int NumberOfFoodToSpawn = 4;


    private SpriteRenderer sRender;
    [SerializeField]
    private Sprite ItemImage;
    [SerializeField]
    private Sprite EmptyImage;


    [SerializeField]
    private GameObject canvasBar;
    [SerializeField]
    private Image durabilityBar;
    private Text remainSourcesText;

    private float GrowNewFoodTimer;
    private float currenttimer;

    // Use this for initialization
    void Start()
    {

        canvasBar.SetActive(false);
    }

    private void Update()
    {
        ////If count is less than cap
        //if (remainingAmount < foodAmountCap)
        //{
        //    //Timer for replenishing Stacks
        //    currenttimer += Time.deltaTime;
        //    if (currenttimer >= GrowNewFoodTimer)
        //    {
        //        remainingAmount++;
        //        currenttimer = 0;
        //    }
        //}
    }

    private void ChangeProgress(float amount)
    {

        progressAmount += amount;
        if (progressAmount >= progressmax)
        {
            progressAmount = 0;
            GenerateResources();
            remainingAmount--;

            if (remainingAmount < 0)
            {
                NoResourcesLeft();

            }
        }
        durabilityBar.fillAmount = progressAmount / progressmax;


        //Show Progress

    }

    private void GenerateResources()
    {
        for (int i = 0; i < NumberOfFoodToSpawn; i++)
        {
            Vector2 metalPos = new Vector2((gameObject.transform.position.x + Random.Range(-2, 2)), transform.position.y + 1f);
            Instantiate(FoodItem, metalPos, FoodItem.transform.rotation);
        }

    }


    private void NoResourcesLeft()
    {
        //get object underneath


        Destroy(gameObject);
    }


    //Show Progress bar
    private void OnMouseOver()
    {
        canvasBar.SetActive(true);

    }
    private void OnMouseExit()
    {
        canvasBar.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.PLAYERWEAPON_TAG)
        {
            WeaponScript playerWeapon = collision.gameObject.GetComponent<WeaponScript>();
            if (playerWeapon.equippedWeapon == WeaponScript.WeaponType.Axe || playerWeapon.equippedWeapon == WeaponScript.WeaponType.Sword)
            {
                ChangeProgress(10);
            }
            else
            {
                ChangeProgress(5);
            }



        }

    }
}

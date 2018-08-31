using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rockVeins : MonoBehaviour {


    private float progressAmount = 0; // Player's progress on hitting
    private float progressmax = 100f;
    [SerializeField]
    public float remainingAmount = 10;

    public GameObject stoneObj;
    public int NumberOfStonesToSpawn = 4;
    public GameObject metalObj;
    public int NumberOfMetalToSpawn = 2;

    [SerializeField]
    private GameObject canvasBar;
    [SerializeField]
    private Image durabilityBar;
    private Text remainSourcesText;

	// Use this for initialization
	void Start () {

        canvasBar.SetActive(false);
	}
	


    private void ChangeProgress(float amount)
    {
        progressAmount += amount;

        if (progressAmount >= progressmax)
        {
            GenerateResources();
            remainingAmount--;

            if (remainingAmount==0)
            {
                //No Resources Left
                NoResourcesLeft();
            }
            else
            progressAmount = 0;

            
        }
        //Show Progress
        durabilityBar.fillAmount = progressAmount / progressmax;

    }

    private void GenerateResources()
    {
        for (int i = 0; i < NumberOfMetalToSpawn; i++)
        {
            Vector2 metalPos  =  new Vector2((gameObject.transform.position.x + Random.Range(-2,2)), transform.position.y + 1f);
            Instantiate(metalObj,metalPos, metalObj.transform.rotation );
        }

        for (int i = 0; i < NumberOfStonesToSpawn; i++)
        {
            Vector2 stonePos = new Vector2((gameObject.transform.position.x + Random.Range(-2, 2)), transform.position.y + 1f);
            Instantiate(stoneObj, stonePos, stoneObj.transform.rotation);
        }
    }


    private void NoResourcesLeft()
    {
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
            if (playerWeapon.equippedWeapon == WeaponScript.WeaponType.Pickaxe)
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

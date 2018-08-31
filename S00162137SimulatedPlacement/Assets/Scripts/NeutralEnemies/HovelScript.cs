using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HovelScript : MonoBehaviour {


    [Header("Object Variables")]
    #region ObjectVariables
    public float Maxhealth;
    public float CurrentHealth;
    public float healthDefendThreshhold;
    private float defendHealth;

    private WaveManager wavemanager;
    private WinCondition wincondition;


    [SerializeField]
    private GameObject guntLing;

    [SerializeField]
    private GameObject brute;

    [SerializeField]
    private GameObject mother;

    [SerializeField]
    private GameObject behemoth;

    #endregion


    [Header("Enemy Variables")]
    #region enemies
    //Enemies to spawn
    public GameObject GibblerSpawn;

    [SerializeField]
    private int MaxGibblerCount;
    public List<GameObject> WaveList = new List<GameObject>();


    [Header("Timer")]
    //Timer Script, for spawning enemies
    public float WaveGaps;
    public float defaultTimer = 15f;
    [SerializeField]
    private float timer = 0;

    private bool DefenceSpawned = false;
    #endregion



    [Header("Info")]
    private bool Damaged = false;
    //InfoDisplay
    public GameObject HovelCanvas;
    public Image healthBar;

    // Use this for initialization
    void Start () {

        HovelCanvas.SetActive(false); // Hides it
        CurrentHealth = Maxhealth;
        defendHealth = (Maxhealth / 100) * 20;
        healthDefendThreshhold = CurrentHealth - 20;

        //For when both destroyed, you win
        wincondition = GameObject.FindGameObjectWithTag(Tags.SCENECONTROLER_TAG).GetComponent<WinCondition>();
        wincondition.AssignHovel(gameObject);

        //used to determine what to spawn
        wavemanager = GameObject.FindGameObjectWithTag(Tags.ENEMY_CONTROLLER_TAG).GetComponent<WaveManager>();

        WaveList.Clear();



        foreach (GameObject enemy in wavemanager.Gibblers)
        {
            WaveList.Add(enemy);
        }

        SpawnGibbler();



	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        //Check if Gibbler's can spawn
        if (WaveList.Count > 0)
        {
            //Timer till Spawning new Gibbler
            timer += Time.deltaTime;
            if (timer >= defaultTimer)
            {
                timer = 0;
                SpawnGibbler();

            }

        }
   


        if (Damaged == true)
        {
            HovelCanvas.SetActive(true);


        }


    }



    public void SpawnGibbler()
    {

      
            GameObject ObjToSpawn;
            ObjToSpawn = WaveList[0];
            Instantiate(GibblerSpawn, transform.position, transform.rotation);
            WaveList.Remove(WaveList[0]);

        if (WaveList.Count <= 0)
        {
            wavemanager.BeginWave();

            wavemanager.MakeWaves();

            WaveList.Clear();
            foreach (GameObject enemy in wavemanager.Gibblers)
            {
                WaveList.Add(enemy);
            }

        }


        // remove from list
        // Instantiate(GibblerSpawn, transform.position,transform.rotation);
        //  reset timer


        //Timer for each unit is equal to wave time spacing /  enemies to spawn
        defaultTimer = WaveGaps / WaveList.Count;




    }


   
    public void HovelDefend()
    {
        float hovelSpawnLeft = transform.position.x - 1f;
        float hovelSpawnRight = transform.position.x + 1f;

        for (int i = 0; i < 3; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(hovelSpawnLeft, hovelSpawnRight), transform.position.y);

            Instantiate(GibblerSpawn, spawnPos, transform.rotation);
            GibblerSpawn.GetComponent<Gibbler>().HovelFrom = gameObject;
            GibblerSpawn.GetComponent<Gibbler>().attacking = true;


        }

        for (int i = 0; i < 2; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(hovelSpawnLeft, hovelSpawnRight), transform.position.y);

            Instantiate(brute, spawnPos, transform.rotation);
            brute.GetComponent<Gibbler>().HovelFrom = gameObject;
            brute.GetComponent<Gibbler>().attacking = true;


        }
        for (int i = 0; i < 1; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(hovelSpawnLeft, hovelSpawnRight), transform.position.y);

            Instantiate(mother, spawnPos, transform.rotation);
            mother.GetComponent<Gibbler>().HovelFrom = gameObject;
            mother.GetComponent<Gibbler>().attacking = true;


        }


    }

    public void HovelDestroy()
    {

        Destroy(gameObject);

    }


    public void TakeDamage(float amount)
    {
        //subtract health
        CurrentHealth = CurrentHealth - amount;
        HovelCanvas.SetActive(true);

        //Update bar
        healthBar.fillAmount = CurrentHealth / Maxhealth;
        if (CurrentHealth <= healthDefendThreshhold)
        {
            healthDefendThreshhold -= 20;
            HovelDefend();
            if (CurrentHealth <= 0)
            {

            }
        }

        if (CurrentHealth <=0)
        {
            wincondition.HovelDestroyed(gameObject);
            HovelDestroy();

        }


        /*
                if (CurrentHealth <= 20 && DefenceSpawned == false)
                {
                    DefenceSpawned = true;
                    HovelDefend();
                }

            */


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.PLAYERWEAPON_TAG)
        {
            string tagCollider = collision.gameObject.tag;

            if (tagCollider == Tags.PLAYERWEAPON_TAG)
            {

                WeaponScript wScript = collision.gameObject.GetComponent<WeaponScript>();

                TakeDamage(wScript.Power);


            }



        }
        else if (collision.gameObject.tag == Tags.MILITIA_TAG)
        {

        }

        else if (collision.gameObject.tag == Tags.ENEMY_PLAYER)
        {

        }
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    [Header("Stat Values")]
    public float jumpPower = 15f;
    public float moveSpeed = 2f;
    private float defaultSpeed;
    public float AttackPower = 5;
    public float KnockBackForce = 5.5f;

    public float Health;
    public float MaxHealth = 100;
    public float Defence = 1;
    private Rigidbody2D rbody;


    [Header("Player Death Variables")]
    public bool IsAlive = true;
    public float RespawnDuration = 22f;
    private float timer = 0f;
    [SerializeField]
    private GameObject DeathPanel;
    [SerializeField]
    private Text DeathTimerText;
    //[HideInInspector]
    public GameObject townHall;




    [Header("UI Elements")]
    public GameObject HPBar;
    public Image HPFillBar; // To fill
    public Color gColor;
    public Color yColor;
    public Color rColor;


    [HideInInspector]
    public enum WeaponType { Axe, Pickaxe, Sword, Bow };
    [HideInInspector]
    public WeaponType equippedWeapon;

    // Use this for initialization
    void Start () {
        defaultSpeed = 2f;
        Health = MaxHealth;
        HPBar.SetActive(false);
        rbody = GetComponent<Rigidbody2D>();

        equippedWeapon = WeaponType.Axe;
        DeathPanel = GameObject.Find(Tags.DirDeathPanel);
        DeathPanel.SetActive(false);
        townHall = GameObject.FindGameObjectWithTag(Tags.TOWNHALL_TAG);

        Camera.main.GetComponent<CameraFollow>().playerObj = gameObject;

    }

    // Update is called once per frame
    void Update () {

        if (IsAlive == false)
        {
            timer -= Time.deltaTime;

            //Update TimerText
            if (timer <=0)
            {
                Respawn();
            }
        }


	}



    public void HealDamage()
    {

    }

    public void TakeDamage(float damageAmount)
    {
        damageAmount -= Defence;
        Health -= damageAmount;


        HPBar.SetActive(true);
        HPFillBar.fillAmount = Health / MaxHealth;
        //HealthBar Colour
        if ((Health <= MaxHealth / 2))
        {
            if (Health > (MaxHealth / 4))
            {
                HPFillBar.color = yColor;
            }
            else if (Health <= MaxHealth / 4)
            {
                HPFillBar.color = rColor;

            }
        }
        if (Health <= 0)
        {
            IsAlive = false;
            Died();
        }

    }

    public void Died()
    {
        RespawnDuration += 2;
        timer = RespawnDuration;
        
        gameObject.transform.position = new Vector2(townHall.transform.position.x, townHall.transform.position.y);
        DeathPanel.SetActive(true);


    }

    public void Respawn()
    {
        IsAlive = true;
        timer = 0f;
        Health = MaxHealth;
        HPBar.SetActive(false);
        DeathPanel.SetActive(false);
    }

    public void KnockedBack(float ForceAmount, Vector3 ForceDir)
    {

        if (ForceDir.x > gameObject.transform.position.x)
        {
            rbody.AddForce((Vector3.left + (Vector3.up / 5)) * ForceAmount, ForceMode2D.Impulse);
        }
        else 
        {
            rbody.AddForce((Vector3.right + (Vector3.up / 5)) * ForceAmount, ForceMode2D.Impulse);
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.ENEMY_TAG)
        {
            EnemyStats estats = collision.gameObject.GetComponent<EnemyStats>();
            Vector3 enemyPos = collision.gameObject.transform.position;
            TakeDamage(estats.Power);
           // KnockedBack(estats.KnockBackForce, enemyPos);

        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MilitiaStats : MonoBehaviour {


    public float MaxHealth;
    //[HideInInspector]
    public float Health;
    public float Power;
    public float Defence;
    public float MoveSpeed;
    public float KnockBackForce;



    [Header("UI Elements")]
    public Image HealthBorder;
    public Image healthBar;
    [SerializeField]
    private Color gColor;
    [SerializeField]
    private Color yColor;
    [SerializeField]
    private Color rColor;


    private Rigidbody2D rBody;


    private void Start()
    {
        Health = MaxHealth;
        rBody = GetComponent<Rigidbody2D>();

        HealthBorder.enabled = false;
    }


    public void HealDamage(float amount)
    {
        Health += amount;
        if (Health >= MaxHealth)
        {
            Health = MaxHealth;
            HealthBorder.enabled = false;
        }

    }


    public void TakeDamage(float amount)
    {
        //Damage amount
        amount -= Defence;
        Health -= amount;

        HealthBorder.enabled = true;
        //HealthBar Colour
        if ((Health <= MaxHealth / 2))
        {
            if (Health > (MaxHealth / 4))
            {
                healthBar.color = yColor;
            }
            else if (Health <= MaxHealth / 4)
            {
                healthBar.color = rColor;

            }
        }

        //Fill amount
        healthBar.fillAmount = Health / MaxHealth;



        if (Health <= 0)
        {
            Died();
        }

    }
  
    public void Died()
    {

    }

    public void KnockedBack(float knockbackAmount, Vector3 KnockPos)
    {


        float RandomKnockbackIncrease =  Random.Range(0f, 2f);


        if (KnockPos.x > gameObject.transform.position.x)
        {
            rBody.AddForce((Vector3.left + Vector3.up) * knockbackAmount, ForceMode2D.Impulse);
        }
        else
        {
            rBody.AddForce((Vector3.right + Vector3.up) * knockbackAmount, ForceMode2D.Impulse);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.ENEMY_TAG)
        {
            EnemyStats eStats = collision.gameObject.GetComponent<EnemyStats>();
            Vector3 unitPos = collision.gameObject.transform.position;

            KnockedBack(eStats.KnockBackForce, unitPos);
            eStats.TakeDamage(Power);

        }


    }

}

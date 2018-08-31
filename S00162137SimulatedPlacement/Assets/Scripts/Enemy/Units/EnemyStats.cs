using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour {


    public float MaxHealth;
    public float Health;
    public float Power;
    public float Defence;
    public float KnockBackForce;

    [Header("UI Elements")]
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

    }





    private void Update()
    {

    }

    public void HealDamage(float amount)
    {

    }


    public void TakeDamage(float amount)
    {
        amount = amount - Defence;
        Health -= amount;

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
 

        healthBar.fillAmount = Health / MaxHealth;



        if (Health <= 0)
        {
            Died();
        }

    }

    public void KnockBacked(float knockForce, Vector3 ForceDir)
    {
        
        if (ForceDir.x > gameObject.transform.position.x)
        {
            rBody.AddForce((Vector3.left + Vector3.up) * (knockForce - Defence), ForceMode2D.Impulse);
        }
        else 
        {
            rBody.AddForce((Vector3.right + Vector3.up) * (knockForce - Defence), ForceMode2D.Impulse);
        }
    }

    public void Died()
    {
        Destroy(gameObject);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.MILITIA_TAG)
        {
            MilitiaStats mStats = collision.gameObject.GetComponent<MilitiaStats>();
            Vector3 unitPos = collision.gameObject.transform.position;

    //        rBody.AddForce((Vector3.right + Vector3.up) * mStats.KnockBackForce, ForceMode2D.Impulse);

            KnockBacked(mStats.KnockBackForce, unitPos);
            mStats.TakeDamage(Power);


        }

    

        if (collision.gameObject.tag == Tags.PLAYERWEAPON_TAG)
        {
            Debug.Log(collision.gameObject.name);
        }


    }


}

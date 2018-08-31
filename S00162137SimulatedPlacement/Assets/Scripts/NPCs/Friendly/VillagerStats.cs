using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillagerStats : MonoBehaviour {

    public float MaxHealth;
    [HideInInspector]
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

    private bool inCombat;

    // Use this for initialization
    void Start () {
		
	}
	

    public void TakeDamage(float damage)
    {
        damage -= Defence;
        Health -= damage;

        inCombat = true;

        if (Health <=0)
        {
          Resources resources = GameObject.FindGameObjectWithTag(Tags.PLAYER_CONTROLLER_TAG).GetComponent<Resources>();

            resources.People--;
            resources.UpdateInfo();



        }


    }





    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag ==  Tags.GIBBLER_TAG)
        {
            GibblerStats gstats = collision.gameObject.GetComponent<GibblerStats>();
            TakeDamage(gstats.Power);

        }
    }


}

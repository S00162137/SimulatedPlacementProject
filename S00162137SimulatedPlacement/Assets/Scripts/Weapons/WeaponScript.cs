using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {


    [SerializeField]
    private PlayerStats pStats;

    public enum WeaponType { Axe, Pickaxe, Sword}
    public WeaponType equippedWeapon;
    public float Power;
    public float KnockbackPower;



    public List<GameObject> enemiesInRange = new List<GameObject>();


    // Use this for initialization
    void Start () {
        pStats = transform.parent.parent.GetComponent<PlayerStats>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.ENEMY_TAG)
        {
            EnemyStats estats = collision.gameObject.GetComponent<EnemyStats>();
            estats.TakeDamage(pStats.AttackPower);
            estats.KnockBacked(pStats.KnockBackForce, gameObject.transform.position);

        }

        if (collision.gameObject.tag == Tags.ENVIRNMENT_TAG)
        {

        }
        else if (collision.gameObject.tag == Tags.GIBBLER_TAG)
        {

            GibblerStats gstats = collision.gameObject.GetComponent<GibblerStats>();
            gstats.TakeDamage(Power);
            gstats.Knockedback(KnockbackPower, transform.position);

        }

        else if (collision.gameObject.tag == Tags.GIBBLERHOVEL_TAG)
        {

        }

    }


}

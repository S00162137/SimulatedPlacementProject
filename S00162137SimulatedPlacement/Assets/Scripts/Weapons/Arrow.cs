using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Rigidbody2D arrowRBody;
    public float PowerDamage = 5f;
    [SerializeField]
    private float FireSpeed = 10f;



    public Vector3 targetPos;

	// Use this for initialization
	void Start () {

        arrowRBody = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        transform.position = transform.up/100;

    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == Tags.GROUND_TAG)
        {
            Destroy(gameObject);
           
        }

        //Hits Gibbler
        else if (collision.gameObject.tag == Tags.GIBBLER_TAG)
        {
            GibblerStats gStats = collision.gameObject.GetComponent<GibblerStats>();
            gStats.TakeDamage(PowerDamage);
        }
     }


}





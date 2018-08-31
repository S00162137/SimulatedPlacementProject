using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class GibblerStats : MonoBehaviour
{
    [Header("Stats")]
    public float MaxHealth = 30;
    public float Health;
    public float moveSpeed = 5f;
    public float KnockbackForce = 4f;
    public float Power = 7f;
    public float Defence = 1;

    [Header("UI Elements")]
    public GameObject canvas;
    public Image healthBar;
    [SerializeField]
    private Color gColor;
    [SerializeField]
    private Color yColor;
    [SerializeField]
    private Color rColor;

    private Rigidbody2D rBody;

    [HideInInspector]
    public HovelScript homeHovel;

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        Health = MaxHealth;
        moveSpeed = Random.Range(moveSpeed - 0.5f, moveSpeed + 0.5f);

        canvas.SetActive(false);
    }

    public void TakeDamage(float damageAmount)
    {
       // damageAmount = damageAmount - Defence;
        Health = Health - damageAmount;

        canvas.SetActive(true);

        healthBar.fillAmount = Health / MaxHealth;

        if (Health <=0)
        {
            GibblerDie();
        }


    }

    public void Knockedback(float knockForce, Vector3 ForceDir)
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

    public void GibblerDie()
    {

            homeHovel = gameObject.GetComponent<Gibbler>().hovelScript;
            Destroy(gameObject);

        
    }

}

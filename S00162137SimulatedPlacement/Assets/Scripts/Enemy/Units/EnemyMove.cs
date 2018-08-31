using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    //Movement Variables
    public bool Advancing = false;
    public bool Following = false;
    public bool Retreating = false;
    public bool advancinvgRight = true;
    public Vector2 advanceDir;
    public Vector2 retreatDir;

    public bool AttackingBuilding = false;

    private float Speed = 4f;
    public EnemyStats enemyStats;

    //Ued to be added to List
    private EnemyUnitManager unitManager;

    Transform tform;
    Transform RallyPoint;

    // Use this for initialization
    void Start()
    {
        tform = GetComponent<Transform>();

        advanceDir = Vector2.left;
        retreatDir = Vector2.right;

        unitManager = GameObject.FindGameObjectWithTag(Tags.ENEMY_CONTROLLER_TAG).GetComponent<EnemyUnitManager>();
        enemyStats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        //While not attacking a building
        if (AttackingBuilding == false)
        {


            if (Advancing == true)
            {
                gameObject.transform.Translate((advanceDir * Speed) * Time.deltaTime);
            }

            else
            {
                gameObject.transform.Translate((retreatDir * Speed) * Time.deltaTime);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }



}

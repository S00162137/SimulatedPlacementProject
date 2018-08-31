using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitManager : MonoBehaviour {


    public List<GameObject> MilitaryUnits = new List<GameObject>();

    public bool Attacking = false;
    public bool Following = false;
    public bool retreating = true;




    public void ToggleAttack()
    {
        Attacking = !Attacking;
        CheckRetreat();
    }

    public void ToggleFollow()
    {
        Following = !Following;
        CheckRetreat();
    }

    public void CheckRetreat()
    {
        if (Following == false && Attacking == false)
        {
            retreating = true;

        }
        else
        {
            retreating = false;
        }


    }

}

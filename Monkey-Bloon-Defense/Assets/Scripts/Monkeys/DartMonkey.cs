using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;

public class DartMonkey : Tower
{
    private void Awake()
    {
        balloon = GameObject.FindGameObjectWithTag("Player");

        rateOfFire = 3.0f;
        timeSinceLastShot = 100.0f;
        //range = 5.0f; //set in inspector
        projectileType = ProjType.Dart;
        popType = PopType.Normal;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (IsInRange() 
            && timeSinceLastShot > rateOfFire
            && CanPop()) //should only shoot when can pop it
        {
            Shoot();
            timeSinceLastShot = 0;
        }
    }


    //this draws the range of the tower
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.gameObject.transform.position, range);
    }
}

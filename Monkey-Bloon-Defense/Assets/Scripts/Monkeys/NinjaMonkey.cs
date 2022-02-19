using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;

public class NinjaMonkey : Tower
{
    private void Start()
    {
        balloon = GameObject.FindGameObjectWithTag("Player");

        rateOfFire = 2.0f;
        timeSinceLastShot = 100.0f;
        //range = 8.0f; // set in inspector
        projectileType = ProjType.Shuriken;
        popType = PopType.Normal | PopType.Camo;
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

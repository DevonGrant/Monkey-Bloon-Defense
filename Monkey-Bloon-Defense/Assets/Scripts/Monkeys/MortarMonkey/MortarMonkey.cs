using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;

public class MortarMonkey : Tower
{
    GameObject crosshair;
    bool targeting = false;
    bool deployed = false;
    private GameObject bomb;
    Animator animator;
    Rigidbody2D chrb;
    CircleCollider2D chcc;
    bool shooting;

    private void Start()
    {
        balloon = GameObject.FindGameObjectWithTag("Player");

        rateOfFire = 5.0f;
        timeSinceLastShot = 100.0f;
        //range = 8.0f; // set in inspector
        projectileType = ProjType.Explosive;
        popType = PopType.Normal | PopType.Lead;

        crosshair = GameObject.Instantiate(GameManager.Instance.crosshairPrefab);
        crosshair.transform.parent = gameObject.transform;
        crosshair.transform.position = this.gameObject.transform.position;
        animator = crosshair.GetComponent<Animator>();
        chrb = crosshair.GetComponent<Rigidbody2D>();
        chcc = crosshair.GetComponent<CircleCollider2D>();

        if (IsInRange())
            targeting = true;
    }

    private void Update()
    {
        //delete the explosion, but make sure it's had screentime
        if (deployed && timeSinceLastShot > 0.10f)
        {
            GameObject.Destroy(bomb);
            bomb = null;
        }

        timeSinceLastShot += Time.deltaTime;

        if (IsInRange()
            && timeSinceLastShot > rateOfFire
            && CanPop()) //should only shoot when can pop it
        {
            Activate();
            timeSinceLastShot = 0;
        }

        if (CanPop() == false
            && chrb != null)
        {
            Destroy(chrb);
            targeting = false;
        }
        if (CanPop()
            && chrb == null)
        {
            ReplenishRigidBody();
        }
    }

    public void Activate()
    {
        animator.SetTrigger("FireExplosive");
        ReplenishRigidBody();
    }
    public void StopTargeting()
    {
        targeting = false;
        shooting = true;
        if (chrb == null)
            return;

        chrb.velocity = Vector3.zero;
        Destroy(chrb);
    }
    public override void Shoot()
    {
        shooting = true;
        timeSinceLastShot = 0;
        bomb = SetProjectileType(projectileType);
        bomb.transform.position = crosshair.transform.position;
        deployed = true;
    }
    public void ResetUnit()
    {
        shooting = false;
        if (IsInRange() && CanPop())
            ReplenishRigidBody();
        crosshair.transform.position = gameObject.transform.position;
    }
    private void ReplenishRigidBody()
    {
        if (chrb != null)
            return;
        if (shooting)
            return;

        chrb = crosshair.AddComponent<Rigidbody2D>();
        chrb.mass = 1.5f;
        chrb.drag = 1f;
        chrb.gravityScale = 0;
    }


    //this draws the range of the tower
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.gameObject.transform.position, range);
    }
}

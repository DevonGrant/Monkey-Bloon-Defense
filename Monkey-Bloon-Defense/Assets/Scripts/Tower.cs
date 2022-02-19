using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;

public abstract class Tower : MonoBehaviour
{
    protected GameObject balloon;
    protected float rateOfFire;
    [SerializeField] protected float timeSinceLastShot;
    public float range;
    protected ProjType projectileType;
    protected PopType popType;

    public virtual void Shoot()
    {
        //Create projectile from prefab based on projectileType
        GameObject g = SetProjectileType(projectileType);
        BackgroundScrollScript.Instance.stationaryObjects.Add(g);
        //get this once
        Projectile gp = g.GetComponent<Projectile>();
        //set direction vector. won't change.
        gp.direction = Vector3.Normalize(balloon.transform.position - gameObject.transform.position);
        g.transform.position = this.gameObject.transform.position;
    }

    protected bool IsInRange()
    {
        if (!balloon) return false;
        if ((balloon.transform.position - gameObject.transform.position).magnitude < range)
            return true;
        return false;
    }
    protected bool CanPop()
    {
        if ((BalloonController.Instance.layerType & popType) == BalloonController.Instance.layerType)
            return true;
        return false;
    }
    protected GameObject SetProjectileType(ProjType pT)
    {
        GameObject g = GameObject.Instantiate(GameManager.Instance.projectilePrefabs[(int)pT]);
        return g;
    }

}

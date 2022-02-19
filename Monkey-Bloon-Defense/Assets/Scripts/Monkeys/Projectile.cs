using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    public ProjType projType;
    public Vector2 direction;
    public PopType popType;
    public BalloonController bc;
    private float speed;
    private Rigidbody2D rb;

    private void Start()
    {
        bc = BalloonController.Instance;
        rb = GetComponent<Rigidbody2D>();
        //set the poptype. can't in inspector (to my knowlege) bc cant do the OR statemets to combine the enums
        
        //sprite = sprites[projType]
        switch (projType)
        {
            default:
            case ProjType.Dart:
                popType = PopType.Normal;
                speed = 3;
                break;
            case ProjType.Shuriken:
                popType = PopType.Normal | PopType.Camo;
                speed = 5;
                break;
            case ProjType.Explosive:
                popType = PopType.Normal | PopType.Lead;
                break;
        }

        //set up the proper rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, new Vector3(0, 0, 1));
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        //do we want to make them all go in a straight line?
        if (projType == ProjType.Dart || projType == ProjType.Shuriken)
        rb.velocity = (direction * speed);
        if (!bc) return;
        if ((gameObject.transform.position - bc.gameObject.transform.position).magnitude > 20.0f)
            BackgroundScrollScript.DisposeOfItem(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;

        if (tag == "Player")
        {
            if ((BalloonController.Instance.layerType & popType) == BalloonController.Instance.layerType) //check again so that random crossfire doesn't pop what it shouldn't
                Pop();
        }
    }

    protected void Pop()
    {
        bc.RemoveLayer();
        BackgroundScrollScript.DisposeOfItem(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, direction);
        Gizmos.DrawWireSphere(transform.position + (Vector3)direction, 0.15f);
    }
}

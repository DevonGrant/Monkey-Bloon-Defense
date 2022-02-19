using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;

public class RoadObstacle : MonoBehaviour
{
    /**************/ public ObstacleType obstacleType;
    // Colors for now, will change to sprites later; also preemptively serialized for when we add sprites later on
    [SerializeField] private Sprite[]    roadObstacleSprites;
    /**************/ private PopType     popType;
    /**************/ private int         maxDrag = 50;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = roadObstacleSprites[(int)obstacleType];

        SetType(obstacleType);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BalloonController bc = BalloonController.Instance;
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

        if (collision.tag == "Player")
        {
            // If the obstacle can pop whatever layer the balloon has, then do so
            // Obstacles should not be "popping" each other
            if ((bc.layerType & popType) == bc.layerType)
            {
                bc.RemoveLayer();
            }
            // Glue doesn't have a popType, so it should just slow down movement
            else if (popType == PopType.None && rb.drag < maxDrag)
            {
                bc.glued = true;
                bc.glueTimerQueue.Enqueue(Time.time + 5.0f);
            }

            BackgroundScrollScript.DisposeOfItem(this.gameObject);
            
        }
    }

    public void SetType(enums.ObstacleType ot)
    {
        obstacleType = ot;
        switch (ot)
        {
            default:
            case ObstacleType.RoadSpikes:
                popType = PopType.Normal | PopType.Camo;
                break;
            case ObstacleType.Glue:
                popType = PopType.None;
                break;
            case ObstacleType.HotSpikes:
                popType = PopType.Normal | PopType.Camo | PopType.Lead;
                break;
        }
    }
}

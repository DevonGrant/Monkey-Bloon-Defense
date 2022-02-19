using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private Sprite[] powerUpSprites;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = powerUpSprites[(int)powerUpType];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BalloonController bc = BalloonController.Instance;

        if (collision.tag == "Player")
        {
            switch (powerUpType)
            {
                case PowerUpType.Shield:
                    bc.AddLayer();
                    break;
                case PowerUpType.Camo:
                    bc.layerType = PopType.Camo;
                    break;
                case PowerUpType.Lead:
                    bc.layerType = PopType.Lead;
                    break;
            }

            BackgroundScrollScript.DisposeOfItem(this.gameObject);
        }
    }

    public void SetType(PowerUpType pt)
    {
        powerUpType = pt;
        gameObject.GetComponent<SpriteRenderer>().sprite = powerUpSprites[(int)powerUpType];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;

public class SpawnPowerUps : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private BackgroundScrollScript bss;

    public const int RATIO_CHANCE_SHIELD = 75;
    public const int RATIO_CHANCE_CAMO = 25;
    public const int RATIO_CHANCE_LEAD = 10;

    public const int RATIO_TOTAL = RATIO_CHANCE_SHIELD
                                 + RATIO_CHANCE_CAMO
                                 + RATIO_CHANCE_LEAD;
    private float timeSinceLastSpwan;
    private float waitTime = 0.75f;
    private float minWait = 5.0f;
    private float maxWait = 8.0f;


    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSpwan = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpwan += Time.deltaTime;
        if (timeSinceLastSpwan > waitTime)
        {
            timeSinceLastSpwan = 0;
            waitTime = Random.Range(minWait, maxWait);
            int x = Random.Range(0, RATIO_TOTAL);

            if ((x -= RATIO_CHANCE_SHIELD) < 0) // Test for Layer
            {
                SpawnPowerUp(gm.powerUpPrefab, PowerUpType.Shield);
            }
            else if ((x -= RATIO_CHANCE_CAMO) < 0) // Test for Camo
            {
                SpawnPowerUp(gm.powerUpPrefab, PowerUpType.Camo);
            }
            else // No need for final if statement (Lead)
            {
                SpawnPowerUp(gm.powerUpPrefab, PowerUpType.Lead);
            }
        }

    }

    private void SpawnPowerUp(GameObject t, PowerUpType pt)
    {
        float isBelow = Random.Range(0, 2); // 0 or 1

        float randY = Random.Range(0f, 2.5f);

        if (isBelow > 0)
        {
            randY *= -1;
        }

        GameObject g = GameObject.Instantiate(t, new Vector3(10, randY, 0), Quaternion.identity);
        g.GetComponent<PowerUp>().SetType(pt);
        bss.stationaryObjects.Add(g);
    }
}

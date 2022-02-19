using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private BackgroundScrollScript bss;

    public const int RATIO_CHANCE_SPIKE = 35;
    public const int RATIO_CHANCE_HOT = 10;
    public const int RATIO_CHANCE_GLUE = 55;

    public const int RATIO_TOTAL = RATIO_CHANCE_SPIKE
                                 + RATIO_CHANCE_HOT
                                 + RATIO_CHANCE_GLUE;
    private float timeSinceLastSpwan;
    private float waitTime = 0.75f;
    private float minWait = 3.0f;
    private float maxWait = 6.0f;


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

            if ((x -= RATIO_CHANCE_SPIKE) < 0) // Test for Spikes
            {
                SpawnObstacle(gm.obstaclePrefab, ObstacleType.RoadSpikes);
            }
            else if ((x -= RATIO_CHANCE_HOT) < 0) // Test for Hot Spikes
            {
                SpawnObstacle(gm.obstaclePrefab, ObstacleType.HotSpikes);
            }
            else // No need for final if statement (Glue)
            {
                SpawnObstacle(gm.obstaclePrefab, ObstacleType.Glue);
            }
        }

    }

    private void SpawnObstacle(GameObject t, ObstacleType ot)
    {
        float isBelow = Random.Range(0, 2); // 0 or 1

        float randY = Random.Range(0f, 2.5f);

        if (isBelow > 0)
        {
            randY *= -1;
        }

        GameObject g = GameObject.Instantiate(t, new Vector3(10, randY, 0), Quaternion.identity);
        g.GetComponent<RoadObstacle>().SetType(ot);
        bss.stationaryObjects.Add(g);
    }
}

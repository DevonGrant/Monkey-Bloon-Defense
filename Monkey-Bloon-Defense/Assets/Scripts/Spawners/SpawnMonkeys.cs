using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonkeys : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private BackgroundScrollScript bss;

    public static readonly int RATIO_CHANCE_DART = 55;
    public static readonly int RATIO_CHANCE_NINJA = 35;
    public static readonly int RATIO_CHANCE_MORTAR = 10;

    public static readonly int RATIO_TOTAL = RATIO_CHANCE_DART
                                           + RATIO_CHANCE_NINJA
                                           + RATIO_CHANCE_MORTAR;
    private float timeSinceLastSpwan;
    private float waitTime = 0.75f;
    private float minWait = 2.0f;
    private float maxWait = 5.0f;


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

            if ((x -= RATIO_CHANCE_DART) < 0) // Test for Dart
            {
                SpawnTower(gm.monkeyPrefabs[0]);
            }
            else if ((x -= RATIO_CHANCE_NINJA) < 0) // Test for Ninja
            {
                SpawnTower(gm.monkeyPrefabs[1]);
            }
            else // No need for final if statement (Mortar)
            {
                SpawnTower(gm.monkeyPrefabs[2]);
            }
        }
        
    }

    private void SpawnTower(GameObject t)
    {
        float isBelow = Random.Range(0, 2); // 0 or 1

        float randY = Random.Range(3.75f, 4.75f);

        if (isBelow > 0)
        {
            randY *= -1;
        }

        GameObject g = GameObject.Instantiate(t, new Vector3(10, randY, 0), Quaternion.identity);
        bss.stationaryObjects.Add(g);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollScript : MonoBehaviour {

    private static BackgroundScrollScript m_instance;
    public static BackgroundScrollScript Instance { get => m_instance; }
    public GameObject backgroundPrefab;

    public List<GameObject> backgrounds;
    public List<Sprite> backgroundSprites;

    public List<GameObject> stationaryObjects;

    public float speed;
    private float distance;
    private float offset = 19.2f; // One square in unity = 10 px

    private void Awake()
    {
        m_instance = this;
        backgrounds = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start() {
        backgrounds.Add(Instantiate(backgroundPrefab));
        stationaryObjects = new List<GameObject>();

        distance = 0f;
    }

    // Update is called once per frame
    void Update() {
        //move the background
        distance += speed * 30 * Time.deltaTime;
        for (int i = 0; i < backgrounds.Count; i++) {
            backgrounds[i].transform.position = new Vector3(-distance + (offset * i), 0, 0);
        }
        //replace the background when the end is reached
        // backgrounds[backgrounds.Count - 1].transform.position.x >= -0.01f && 
        if (backgrounds[backgrounds.Count - 1].transform.position.x <= offset + 0.01f) {

            // Give new background GO a random background tile sprite
            GameObject newBackground = Instantiate(backgroundPrefab);
            SpriteRenderer spriteRenderer = newBackground.GetComponent<SpriteRenderer>();
            int index = Random.Range(0, 4);
            spriteRenderer.sprite = backgroundSprites[index];

            backgrounds.Add(newBackground);
        }
        //detroy old backgrounds
        //todo: remove old backgrounds, reduce distance value to account to new background positions
        for (int i = 0; i < backgrounds.Count; i++) {
            if (backgrounds[i].transform.position.x <= -27.0f) {
                GameObject deletedObject = backgrounds[i];
                backgrounds.Remove(deletedObject);
                Destroy(deletedObject);
                distance -= offset;
            }
        }

        //move the objects in the scene
        for (int i = 0; i < stationaryObjects.Count; i++) {
            stationaryObjects[i].transform.position = new Vector3(stationaryObjects[i].transform.position.x - speed * 30 * Time.deltaTime, 
                                                                  stationaryObjects[i].transform.position.y,
                                                                  stationaryObjects[i].transform.position.z);

            if (stationaryObjects[i].transform.position.x < -20)
            {
                DisposeOfItem(stationaryObjects[i]);
            }
        }        
    }
    public static void DisposeOfItem(GameObject g)
    {
        BackgroundScrollScript.Instance.stationaryObjects.Remove(g);
        GameObject.Destroy(g);
    }
}

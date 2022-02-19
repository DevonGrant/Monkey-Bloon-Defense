using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;

public class BalloonController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject spriteGO;
    [SerializeField] private GameObject glueSoundObject;
    // Audio
    private AudioSource glueSFX;
    private AudioSource popSound;

    private static BalloonController m_instance = null;
    public static BalloonController Instance { get => m_instance; }
    private Color32[] balloonColors; // Colors for now, will be sprites later
    private Rigidbody2D balloonRB;
    private SpriteRenderer spriteRenderer;
    private int layers;
    private int maxLayers = 3;
    public PopType layerType;
    public Queue<float> glueTimerQueue;
    private float glueDrag = 5.0f;
    public bool glued = false;
    private float initialDrag = 2.0f;

    public int Layers { get => layers; }

    private void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }

        balloonColors = new Color32[3];
        glueTimerQueue = new Queue<float>();

        // Different sprites for each layer type
        balloonColors[0] = new Color32(255, 208, 247, 255); // light pink | normal sprite
        balloonColors[1] = new Color32(94, 128, 44, 255);   // muddy green | camo sprite
        balloonColors[2] = new Color32(152, 152, 152, 255); // grey | lead sprite
    }

    void Start()
    {
        glueSFX = glueSoundObject.GetComponent<AudioSource>();
        layerType = PopType.Normal;
        layers = 0;
        balloonRB = GetComponent<Rigidbody2D>();
        spriteRenderer = spriteGO.GetComponent<SpriteRenderer>();
        popSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Add slow down effect (stackable) based on amount of glue ran over
        if (glueTimerQueue.Count > 0 && Time.time >= glueTimerQueue.Peek())
            glueTimerQueue.Dequeue();

        if(glued)
        {
            glueSFX.Play();
            glued = false;
        }

        balloonRB.drag = (glueTimerQueue.Count * glueDrag) + initialDrag;

        MoveBalloon();
        DisplayLayer();
    }

    /// <summary>
    /// Controls balloon movement
    /// </summary>
    private void MoveBalloon()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector2 force = new Vector2(xInput, yInput) * speed * 200 * Time.deltaTime;

        balloonRB.AddForce(force);
    }

    /// <summary>
    /// Sets the balloon to the right color depending on
    /// how many layers it has
    /// </summary>
    private void DisplayLayer()
    {
        int spriteIndex = (int)layerType / 2;

        if(layerType == PopType.Normal)
        {
            switch(layers)
            {
                case 0:
                    // display default balloon sprite color (light pink)
                    spriteRenderer.color = new Color32(255, 208, 247, 255);
                    break;
                case 1:
                    // display balloon with one extra layer (light blue)
                    spriteRenderer.color = new Color32(208, 247, 255, 255);
                    break;
                case 2:
                    // display balloon with two extra layers (light green)
                    spriteRenderer.color = new Color32(185, 255, 121, 255);
                    break;
            }
        }
        else
        {
            spriteRenderer.color = balloonColors[spriteIndex]; // We dont have sprites yet, so we're using this for now
        }
    }

    public void AddLayer()
    {
        if(layers + 1 < maxLayers)
        {
            layers++;
        }
    }

    public void RemoveLayer()
    {
        popSound.Play();

        switch (layerType)
        {
            case PopType.Normal:
                layers--;
                if (layers < 0)
                    GameManager.Instance.currentState = GameState.GAME_OVER;
                break;

            case PopType.Camo:
            case PopType.Lead:
                layerType = PopType.Normal;
                break;
        }
    }
}

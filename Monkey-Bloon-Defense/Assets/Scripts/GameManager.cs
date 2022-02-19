using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using enums;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance = null;
    public static GameManager Instance { get => m_instance; }
    public GameObject[] projectilePrefabs;
    public GameObject[] monkeyPrefabs;
    public GameObject obstaclePrefab;
    public GameObject powerUpPrefab;
    public Sprite[] powerUpSprites;
    public GameObject crosshairPrefab;
    public GameState currentState;
    public GameObject balloon;

    // UI references
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pausedUI;

    int score = 0;
    int timeIterator = 0;

    public Text scoreTextbox;

    private void Awake()
    {
        if (m_instance == null || m_instance != this)
        {
            m_instance = this;
        }
    }

    private void Start()
    {
        gameOverUI.SetActive(false);
        pausedUI.SetActive(false);
        Time.timeScale = 1;

        // Starting state of the scene
        currentState = GameState.GAME;
    }

    private void FixedUpdate() {
        timeIterator++;
        if (timeIterator == 50) {
            timeIterator = 0;
            score++;
            scoreTextbox.text = "Score: " + score;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentState = GameState.PAUSED;
        }

        switch(currentState)
        {
            case GameState.GAME_OVER:
                GameOver();
                break;
            case GameState.PAUSED:
                Paused();
                break;
            default:
                break;
        }
    }

    private void GameOver()
    {
        Transform[] allChildren = balloon.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            SpriteRenderer sprite = child.gameObject.GetComponent<SpriteRenderer>();

            if(sprite != null)
                sprite.enabled = false;
        }

        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    private void Paused()
    {
        Time.timeScale = 0;
        pausedUI.SetActive(true);
    }
}

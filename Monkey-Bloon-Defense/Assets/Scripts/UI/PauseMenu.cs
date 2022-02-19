using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using enums;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonMenu;
    [SerializeField] private Button buttonQuit;

    // Start is called before the first frame update
    void Start()
    {
        buttonResume.onClick.AddListener(() =>
        {
            GameManager.Instance.currentState = GameState.GAME;
            Time.timeScale = 1;
            gameObject.SetActive(false);
        });

        buttonMenu.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        });

        buttonQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}

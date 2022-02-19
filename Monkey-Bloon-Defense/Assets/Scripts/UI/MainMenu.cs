using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button buttonStart;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private Button buttonOptns;
    [SerializeField] private GameObject ui_options;
    [SerializeField] private GameObject container_menuButtons;

    private void Awake()
    {
        ui_options.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonStart.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });

        buttonOptns.onClick.AddListener(() =>
        {
            ui_options.SetActive(true);
            container_menuButtons.SetActive(false);
        });

        // Quit button should exit the game
        buttonQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}

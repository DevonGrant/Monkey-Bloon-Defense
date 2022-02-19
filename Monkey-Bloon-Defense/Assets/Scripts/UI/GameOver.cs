using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button buttonRetry;
    [SerializeField] private Button buttonReturn;

    // Start is called before the first frame update
    void Start()
    {
        buttonRetry.onClick.AddListener(() =>
        {
            // For now, this just reloads the game scene. 
            // Having a reset function that resets all the values would be very helpful.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        buttonReturn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        });
    }
}

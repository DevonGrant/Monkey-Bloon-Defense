using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    [SerializeField] private Button buttonMenu;
    [SerializeField] private GameObject container_menuButtons;
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = MusicManager.Instance.audioVolume;

        buttonMenu.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            container_menuButtons.SetActive(true);
        });

    }

    void Update()
    {
        MusicManager.Instance.audioVolume = volumeSlider.value;
    }
}

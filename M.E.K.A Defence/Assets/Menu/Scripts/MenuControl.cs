using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{

    public GameObject panel;
    public GameObject backButton;
    public GameObject mainCanvas;


    void Start()
    {
        panel = GameObject.Find("ControlScreen");
        backButton = GameObject.Find("BackButton");

        mainCanvas = GameObject.Find("MainMenuScreen");

        HideControls();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ShowControls()
    {
        panel.SetActive(true);
        backButton.SetActive(true);


        mainCanvas.SetActive(false);
    }

    public void HideControls()
    {
        panel.SetActive(false);
        backButton.SetActive(false);

        mainCanvas.SetActive(true);
    }


    public void Exit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{

    public GameObject panel;
    public GameObject backButton;

    public GameObject creditPanel;
    public GameObject creditBackButton;

    public GameObject mainCanvas;


    void Start()
    {
        //Controls
        panel = GameObject.Find("ControlScreen");
        backButton = GameObject.Find("BackButton");

        //Credits
        creditPanel = GameObject.Find("CreditScreen");
        creditBackButton = GameObject.Find("BackButton2");

        mainCanvas = GameObject.Find("MainMenuScreen");

        HideControls();
        HideCredits();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Final Scene");

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

    public void PlayCredits()
    {
        creditPanel.SetActive(true);
        creditBackButton.SetActive(true);

        mainCanvas.SetActive(false);
    }

    public void HideCredits()
    {
        creditPanel.SetActive(false);
        creditBackButton.SetActive(false);

        mainCanvas.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

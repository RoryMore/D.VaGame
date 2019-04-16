using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ShowControls()
    {
       //Enable control canvas
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    public void ReturntoMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}

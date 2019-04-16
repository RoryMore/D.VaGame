using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
        PlayerModifierManager.Instance.ResetModifiers();
    }
    
}

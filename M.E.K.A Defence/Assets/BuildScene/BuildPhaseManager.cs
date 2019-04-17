using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildPhaseManager : MonoBehaviour
{
    
    public void StartWave()
    {
        //CHANGE THIS TO GAME SCENE
        SceneManager.LoadScene("ShmupScene");
    }

}


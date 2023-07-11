using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void OnStart()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void OnExit()
    {
        Application.Quit();
    }
}

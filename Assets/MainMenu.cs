using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject SettingMenuUI;
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Home");

    }

    public void SettingGame()
    {
        SettingMenuUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = true;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

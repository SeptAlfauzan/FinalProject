using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
public static bool GameIsPaused = false;

    public GameObject pauseMenuUI,SettingMenuUI,background, gameOverUI;
    [Header("Button Event System")]
    public GameObject firstButton;
    public bool isGameOver = false;
    private bool pressBackToMenu = false;
    [SerializeField] LoadingScreen loadingScreen;
    private void Start() {
        Resume();
        background.SetActive(false);
    }

    void Update()
    {
        if(isGameOver && !pressBackToMenu) {
            gameOverUI.SetActive(true);
            gameOverUI.GetComponent<GameOver>().GameOverOn();
            return;
        }

        if(Input.GetKeyDown("escape")){
            if (GameIsPaused) Resume();
            else Pause();
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        background.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
     public void SettingGame()
    {
        SettingMenuUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
        background.SetActive(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        loadingScreen.gameObject.SetActive(true);
        Time.timeScale = 1f;
        loadingScreen.LoadScene("Menu2");
    }

    public void BackToMainMenu(){
        isGameOver = false;
        pressBackToMenu = true;
        gameOverUI.SetActive(false);
        LoadMenu();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverUI;
    [SerializeField] LoadingScreen loadingScreen;
    [SerializeField] GameObject firstButton;
    [SerializeField] MainMenu mainMenu;
    [SerializeField] QuestData questData;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI taskCompletedText;
    void Start()
    {
        gameOverUI.SetActive(false);
    }

    private void Awake() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    // Update is called once per frame
    public void GameOverOn(){
        int completedTask = 0;
        foreach (Quest quest in questData.quests)
        {
            if(quest.completed) completedTask += 1;
        }
        if(completedTask == questData.quests.Count) titleText.text = "CONGRATS!";


        Debug.Log("complete task "+completedTask);
        Time.timeScale = 0;
        taskCompletedText.text = completedTask.ToString(); 
        gameOverUI.SetActive(true);
    }
    public void ReplayGame(){ 
        mainMenu.ResetSceneInfoData();
        loadingScreen.gameObject.SetActive(true);
        Time.timeScale = 1f;
        loadingScreen.LoadScene("Home");
    }
   
}

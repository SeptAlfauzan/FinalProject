using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestContainerController : MonoBehaviour
{
    [SerializeField] GameObject questListButtonPrefab;
    [SerializeField] GameObject questListContainer;
    [SerializeField] GameObject parentContainer;
    // [SerializeField] QuestController questController;
    private bool isActive = false;
    [SerializeField] QuestData questData;
    [SerializeField] SceneInfo sceneInfo;
    [SerializeField] GameOver gameOver;
    [SerializeField] int failedQuest;
    [SerializeField] int completeQuest;
    
    [Header("Event System Button Selected")]
    [SerializeField] private GameObject firstQuestButton, exitButton;
    private void Update() {
        if(Input.GetButtonDown("Task")){
            parentContainer.SetActive(!isActive);
            isActive = !isActive;
            Time.timeScale = Time.timeScale == 1? 0 : 1;
        }

        UpdateCompletedQuests();
        UpdateFailedQuests();

        // Debug.Log("failed and completed quests" + failedQuest + completeQuest);

        if(failedQuest + completeQuest == questData.quests.Count) gameOver.GameOverOn();
        if(failedQuest == questData.quests.Count){
            Debug.Log("failed quest");
            gameOver.GameOverOn();
        }
        if(completeQuest == questData.quests.Count){
            Debug.Log("completed quest");
            gameOver.GameOverOn();
        }
    }
    // for onclick functionality
    public void CloseQuestContainer(){
        parentContainer.SetActive(false);
        isActive = false;
        Time.timeScale = 1;
    }
    public void OpenQuestContainer(){
        parentContainer.SetActive(true);
        isActive = true;
        Time.timeScale = 0;

        EventSystem.current.SetSelectedGameObject(null);//reset selected button input system
        EventSystem.current.SetSelectedGameObject(firstQuestButton);//set first input system
    }

    public void UpdateFailedQuest(){
        int currentFailedQuest = 0;
        foreach(Quest quest in questData.quests){
            if(sceneInfo.gameTime > quest.dayDeadline && !quest.completed){
                currentFailedQuest += 1;
                quest.notCompleted = true;
            }
        }
        if(failedQuest != currentFailedQuest) failedQuest = currentFailedQuest;
    }

    public void UpdateCompletedQuests(){
        completeQuest = 0;
        // int currentFailedQuest = 0;
        foreach(Quest quest in questData.quests){
            if(quest.completed) completeQuest += 1;
        }
    }
    public void UpdateFailedQuests(){
        failedQuest = 0;
        // int currentFailedQuest = 0;
        foreach(Quest quest in questData.quests){
            if(quest.notCompleted) failedQuest += 1;
        }
    }
}

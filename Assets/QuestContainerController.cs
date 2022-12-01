using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestContainerController : MonoBehaviour
{
    [SerializeField] GameObject questListButtonPrefab;
    [SerializeField] GameObject questListContainer;
    [SerializeField] GameObject parentContainer;
    private bool isActive = false;
    [SerializeField] QuestData questData;
    
    [Header("Event System Button Selected")]
    [SerializeField] private GameObject firstQuestButton, exitButton;
    private void Start() {
        int index = 0;
        foreach (Quest item in questData.quests){
            GameObject questListButton = Instantiate(questListButtonPrefab, questListContainer.transform);
            QuestButton questButton = questListButton.GetComponent<QuestButton>();
            
            questButton.objectiveItemIconObj.texture = item.itemData.icon;
            questButton.objectiveTextObj.text = "Sell " + item.amountNeed + " " + item.itemData.name + "s";
            questButton.targetAchievedTextObj.text = item.amountGiven + "/" + item.amountNeed;
            
            if(index == 0) firstQuestButton = questListButton;//set first button of input system
            index ++;
        }
    }
    private void Update() {
        if(Input.GetButtonDown("Task")){
            parentContainer.SetActive(!isActive);
            isActive = !isActive;
            Time.timeScale = Time.timeScale == 1? 0 : 1;
        }
    }
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
}

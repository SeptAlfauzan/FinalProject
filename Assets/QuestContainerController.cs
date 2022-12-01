using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestContainerController : MonoBehaviour
{
    [SerializeField] GameObject questListButtonPrefab;
    [SerializeField] GameObject questListContainer;
    [SerializeField] GameObject parentContainer;
    private bool isActive = false;
    [SerializeField] QuestData questData;
    private void Start() {
        foreach (Quest item in questData.quests){
            GameObject questListButton = Instantiate(questListButtonPrefab, questListContainer.transform);
            QuestButton questButton = questListButton.GetComponent<QuestButton>();
            
            questButton.objectiveItemIconObj.texture = item.itemData.icon;
            questButton.objectiveTextObj.text = "Sell " + item.amountNeed + " " + item.itemData.name + "s";
            questButton.targetAchievedTextObj.text = item.amountGiven + "/" + item.amountNeed;
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
    }
}

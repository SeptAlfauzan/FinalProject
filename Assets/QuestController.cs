using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour {
    public QuestData questData;
    public GameObject questListButtonPrefab;
    private List<GameObject> buttons = new List<GameObject>();
    [SerializeField] GameObject container;
    private void OnEnable() {
        Debug.Log(questData.quests.Count);
        ResetButton();
        foreach (Quest item in questData.quests){
            GameObject questListButton = Instantiate(questListButtonPrefab, container.transform);
            QuestButton questButton = questListButton.GetComponent<QuestButton>();
            
            questButton.objectiveItemIconObj.texture = item.itemData.icon;
            questButton.objectiveTextObj.text = "Sell " + item.amountNeed + " " + item.itemData.name + "s";
            questButton.targetAchievedTextObj.text = item.amountGiven + "/" + item.amountNeed;
            
            buttons.Add(questListButton);
            // if(index == 0) firstQuestButton = questListButton;//set first button of input system
            // index ++;
        }
    }
    private void ResetButton(){
        foreach (GameObject button in buttons){
            Destroy(button);
        }
        buttons.Clear();
    }
    private void OnDisable() {
        Debug.Log("on disabled");
        // ResetButton();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestAlertController : MonoBehaviour
{
    [SerializeField] GameObject questAlert;
    [SerializeField] Text questAlertText;
    [SerializeField] Animator questAlertAnimator;
    [SerializeField] QuestData questData;
    int completedQuest = 0;
    int previousCompletedQuest = 0;
    private void Start() {
        foreach (Quest quest in questData.quests){
            if(quest.completed) previousCompletedQuest += 1;
        }
    }

    private void LateUpdate() {
        completedQuest = 0;
        foreach (Quest quest in questData.quests)
        {
            if(quest.completed) completedQuest += 1;
        }

        if(completedQuest > previousCompletedQuest){
            OpenAlert();
            previousCompletedQuest = completedQuest;
        }
    }


    private void OpenAlert(){
        questAlertText.text = completedQuest.ToString() + " Quest completed";
        questAlert.SetActive(true);
        questAlertAnimator.SetTrigger("Open Alert");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest{
    public ItemData itemData;
    public string name;
    public int amountNeed;
    public int amountGiven;
    public int dayDeadline;
    public bool completed;
    public bool notCompleted;
}

[CreateAssetMenu(fileName = "Quest List Info")]
public class QuestData : ScriptableObject
{
    public List<ItemData> possibleQuestRequestItem;
    public List<Quest> quests;
}

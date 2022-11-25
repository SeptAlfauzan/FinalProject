using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest{
    public PlantItemData plant;
    public int amountNeed;
    public int amountGiven;
    public int dayDeadline;
    public bool completed;
}

[CreateAssetMenu(fileName = "Quest List Info")]
public class QuestData : ScriptableObject
{
    public List<Quest> quests;
}

using UnityEngine;

[CreateAssetMenu(menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    // icon
    // prefab
    public GameObject prefabData;
    public Texture icon;
    public int prize;
    public int energyWhenEat = 0;
}

using System.Collections;
using UnityEngine;
public class Planting : MonoBehaviour
{
    [SerializeField] public ItemButton itemInHand;
    [SerializeField] public GameObject itemInHandObj;
    [SerializeField] public int itemInHandInventoryIndex;
    public GameObject GetItemInHandObj(){
        if(itemInHand == null) return null;
        return itemInHand.dataItem.GetItemData().prefabData;
    }
    public int GetItemInHandLength(){
        return itemInHand.GetItemLength();
    }
    public void SetItemInHand(ItemButton itemInHand, int itemIndexInventory){
        this.itemInHand = itemInHand;
        this.itemInHandInventoryIndex = itemIndexInventory; 
    }
}

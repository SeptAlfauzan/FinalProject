using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

class ItemButton{
    public CollectibleItem dataItem;
    public GameObject buttonItem;
    public ItemButton(CollectibleItem dataItem, GameObject buttonItem){
        this.dataItem = dataItem;    
        this.buttonItem = buttonItem;

        SetIcon(this.dataItem.GetItemData().icon);
    }
    private void SetIcon(Texture image){
        this.buttonItem.GetComponentInChildren<RawImage>().enabled = true;
        this.buttonItem.transform.GetChild(0).GetComponent<RawImage>().texture = image;
    }
    public void SetItemLength(int length){
        this.dataItem.SetLength(length);

        GameObject numberTextContainerObj = this.buttonItem.transform.GetChild(1).gameObject;
        numberTextContainerObj.SetActive(true);//set active gameobject
        // Debug.Log(numberTextContainerObj.transform.GetChild(0).GetComponent<TextMeshPro>());
        Debug.Log(numberTextContainerObj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = length.ToString());

        
        // .SetText("length.ToString()");//set number
    }
    public int GetItemLength(){
        return dataItem.GetLength();
    }
}

public class Inventory : MonoBehaviour
{
    private Dictionary<string, CollectibleItem> items = new Dictionary<string, CollectibleItem>();
    [SerializeField] private Dictionary<string, ItemButton> itemInButtons = new Dictionary<string, ItemButton>();
    [SerializeField] private int maximumInventory;
    [SerializeField] private GameObject buttonItemInventory;

    // private void Awake() {
    //     itemButtons = GameObject.FindGameObjectsWithTag("InventoryItemButton");
    //     dataButtons = new Dictionary<string, ItemButton>(itemButtons.Length);
    // } 
    public void SetItems(Dictionary<string, CollectibleItem> items){
        this.items = items; 
    }
    public void Add(string key, GameObject item){
        int numberItem = items.ContainsKey(key)? items[key].GetLength() + 1 : 1;
                // int numberItem = 1;
        if(numberItem == 1){
            CollectibleItem collectibleItem = new CollectibleItem(numberItem, item.GetComponent<Collectible>().itemData);
            items.Add(key, collectibleItem);
            //intansiasi button inventory
            GameObject newItemButton = Instantiate(buttonItemInventory, this.transform);
            // Texture icon = item.GetComponent<Collectible>().itemData.icon;

            itemInButtons.Add(key, new ItemButton(collectibleItem, newItemButton));
            // SetIconItemInventory(icon);
        }else{
            items[key].SetLength(numberItem);
            itemInButtons[key].SetItemLength(numberItem);
        }
    }
    public Dictionary<string, CollectibleItem> GetItems(){
        return items;
    }
    public CollectibleItem GetItem(string key){
        return items[key];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

class ItemButton{
    public CollectibleItem dataItem;
    public GameObject button;
    public ItemButton(CollectibleItem dataItem, GameObject button){
        this.dataItem = dataItem;    
        this.button = button;

        SetIcon(this.dataItem.GetItemData().icon);
    }
    private void SetIcon(Texture image){
        this.button.GetComponentInChildren<RawImage>().enabled = true;
        this.button.transform.GetChild(0).GetComponent<RawImage>().texture = image;
    }
    public void SetItemLength(int length){
        this.dataItem.SetLength(length);

        GameObject numberTextContainerObj = this.button.transform.GetChild(1).gameObject;
        numberTextContainerObj.SetActive(true);//set active gameobject
     
        Debug.Log(numberTextContainerObj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = length.ToString());
    }
    public int GetItemLength(){
        return dataItem.GetLength();
    }
}

public class Inventory : MonoBehaviour
{
    private Dictionary<string, CollectibleItem> items = new Dictionary<string, CollectibleItem>();
    [SerializeField] private Dictionary<string, ItemButton> itemInButtons = new Dictionary<string, ItemButton>();
    [SerializeField] private List<string> itemNameInInventory;//to keep tract every item name in inventory
    [SerializeField] private int maximumInventory;
    [SerializeField] private GameObject buttonItemInventory;
    private int indexStarted = 0;
    private GameObject previousSelectedBorder;
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
            itemInButtons.Add(key, new ItemButton(collectibleItem, newItemButton));
            itemNameInInventory.Add(key);
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
    public void IncreaseIndexStarted(){
        indexStarted += 1;
    }
    public void DecreaseIndexStarted(){
        indexStarted -= 1;
    }

    public void InventoryControl(){
        try{
            if(Input.GetKeyDown(KeyCode.Alpha1)) SelectItemAt(1);
            if(Input.GetKeyDown(KeyCode.Alpha2)) SelectItemAt(2);
            if(Input.GetKeyDown(KeyCode.Alpha3)) SelectItemAt(3);
            if(Input.GetKeyDown(KeyCode.Alpha4)) SelectItemAt(4);
            if(Input.GetKeyDown(KeyCode.Alpha5)) SelectItemAt(5);
            if(Input.GetKeyDown(KeyCode.Alpha6)) SelectItemAt(6);
        } catch (System.Exception e){
            Debug.Log(e.Message);
        }
    }
    public void DropItemAt(int index){
        index -= 1;
        string itemName = itemNameInInventory[index];
        GameObject buttonObj = itemInButtons[itemName].button;//get button game object
        itemNameInInventory.RemoveAt(index); //remove tracked list name  
        Debug.Log(itemNameInInventory);

        Destroy(buttonObj);
    }
    public void SelectItemAt(int index){
        index -= 1;
        string itemName = itemNameInInventory[index];
        GameObject selected = itemInButtons[itemName].button;//get button game object

        GameObject selectedBorder = selected.transform.GetChild(2).gameObject;

        if(previousSelectedBorder)              previousSelectedBorder.SetActive(false);
        
        selectedBorder.SetActive(true);

        previousSelectedBorder = selectedBorder;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
struct MenuItem{
    public ItemData itemData;
    public string itemName;
    public string itemPrize;
    public GameObject seedBag;
    public ItemBuyedTypes type;
}
public class MarketController : MonoBehaviour
{
    [SerializeField] List<MenuItem> menuItems;
    [SerializeField] GameObject containerMenu;
    [SerializeField] GameObject canvasHUD;
    [SerializeField] GameObject areaMarket;
    [SerializeField] GameObject marketMenuUI;
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject prefabMenuListButton;
    [SerializeField] GameObject alertUI;
    [SerializeField] Player player;
    private bool isEnterMarket = false;
    private bool isOpenUI = false;

    [Header("Input System Buttons")]
    [SerializeField] GameObject firstMenuSelected;
    [SerializeField] GameObject closeButton;
    private void Start() {
        int index = 0;
        closeButton.GetComponent<Button>().onClick.AddListener(CloseMenuUI);
        
        foreach (MenuItem item in menuItems){
            GameObject menuListButton = prefabMenuListButton;
            // refactor this
            menuListButton.GetComponent<MarketMenuButton>().itemName = item.itemName;
            menuListButton.GetComponent<MarketMenuButton>().itemPrize = item.itemData.prize.ToString() + " Gold";
            menuListButton.GetComponent<MarketMenuButton>().itemData = item.itemData;
            menuListButton.GetComponent<MarketMenuButton>().texture = item.itemData.icon;
            menuListButton.GetComponent<MarketMenuButton>().player = player;
            menuListButton.GetComponent<MarketMenuButton>().collectible = item.seedBag;
            menuListButton.GetComponent<MarketMenuButton>().type = item.type;
            menuListButton.GetComponent<MarketMenuButton>().alert = alertUI;
            
            GameObject buttonMenu = Instantiate(menuListButton, containerMenu.transform);
            if(index == 0) firstMenuSelected = buttonMenu;
            index ++;
        }
    }
    private void Update() {
        if(isEnterMarket && Input.GetButton("Interact")) SetOpenMenuUI(true);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            isEnterMarket = true;
            other.gameObject.GetComponent<Player>().onArea = true;
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player"){
            isEnterMarket = true;
            other.gameObject.GetComponent<Player>().onArea = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            isEnterMarket = false;
            other.gameObject.GetComponent<Player>().onArea = false;
        }
    }
    private void SetOpenMenuUI(bool status){
        // canvasHUD.SetActive(!status);
        // if(status) inventoryUI.SetActive(true);
        marketMenuUI.SetActive(status);

        isOpenUI = status;
        Time.timeScale = isOpenUI? 0 : 1;

        if(alertUI.activeInHierarchy){
            EventSystem.current.SetSelectedGameObject(null);//reset current selected input system
            EventSystem.current.SetSelectedGameObject(alertUI.GetComponent<AlertController>().closeButton);//set current selected input system
        }else{
            EventSystem.current.SetSelectedGameObject(null);//reset current selected input system
            EventSystem.current.SetSelectedGameObject(firstMenuSelected);//set current selected input system
        }
    }
    public void CloseMenuUI(){
        SetOpenMenuUI(false);
    }
}

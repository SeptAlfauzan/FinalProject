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
}
public class MarketController : MonoBehaviour
{
    [SerializeField] List<MenuItem> menuItems;
    [SerializeField] GameObject containerMenu;
    [SerializeField] GameObject canvasHUD;
    [SerializeField] GameObject areaMarket;
    [SerializeField] GameObject marketMenuUI;
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
            menuListButton.GetComponent<MarketMenuButton>().itemPrize = item.itemPrize;
            menuListButton.GetComponent<MarketMenuButton>().itemData = item.itemData;
            menuListButton.GetComponent<MarketMenuButton>().texture = item.itemData.icon;
            menuListButton.GetComponent<MarketMenuButton>().player = player;
            menuListButton.GetComponent<MarketMenuButton>().collectible = item.seedBag;
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
        if(other.gameObject.tag == "Player") isEnterMarket = true;
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player") isEnterMarket = true;
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") isEnterMarket = false;
    }
    private void SetOpenMenuUI(bool status){
        canvasHUD.SetActive(!status);
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

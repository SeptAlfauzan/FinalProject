using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Start() {
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
            
            Instantiate(menuListButton, containerMenu.transform);
        }
    }
    private void Update() {
        Debug.Log(isEnterMarket);
        Debug.Log(Input.GetButton("Interact"));
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
    }
    public void CloseMenuUI(){
        SetOpenMenuUI(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum ItemBuyedTypes{
    Seed, Food
}
public class MarketMenuButton: MonoBehaviour
{
    public string itemName; 
    public ItemBuyedTypes type = ItemBuyedTypes.Seed;
    public string itemPrize;
    public ItemData itemData;
    public RawImage icon;
    public Texture texture;
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemPrizeText;
    [SerializeField] Text itemEnergyGain;
    [SerializeField] SceneInfo sceneInfo;
    [SerializeField] AudioSource moneyClingSFX;
    public Player player;
    public GameObject collectible;
    public GameObject alert;

    [Header("Input System Buttons")]
    [SerializeField] GameObject closeAlertButton;
    
    void Start(){
        itemNameText.text = itemName;
        itemPrizeText.text = itemPrize;
        icon.texture = texture;

        if(type == ItemBuyedTypes.Food){
            itemEnergyGain.text = "+" + itemData.energyWhenEat.ToString() + " Energy"; 
        }else{
            itemEnergyGain.enabled = false;
        }
        this.GetComponent<Button>().onClick.AddListener(delegate {
            OnClick();
        });
    }
    private void OnClick() {
        Debug.Log("kontol");
        if(sceneInfo.money < itemData.prize){
            ShowAlertBuying();
            return;
        }

        if(!player) return;

        PlaySFX();
        sceneInfo.money -= itemData.prize;
        if(type == ItemBuyedTypes.Food){
            sceneInfo.playerStamina += itemData.energyWhenEat;
        }else{
            GameObject itemBuyed = Instantiate(collectible);
            player.collectibleItem = itemBuyed;
            player.PickUpItem();
            Destroy(itemBuyed);
        }
    }
    private void ShowAlertBuying(){
        alert.SetActive(true);
    }
    private void PlaySFX(){
        if(!moneyClingSFX.isPlaying) moneyClingSFX.Play();
    }
}

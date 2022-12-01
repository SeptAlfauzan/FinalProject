using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MarketMenuButton: MonoBehaviour
{
    public string itemName; 
    public string itemPrize;
    public ItemData itemData;
    public RawImage icon;
    public Texture texture;
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemPrizeText;
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

        this.GetComponent<Button>().onClick.AddListener(delegate {
            OnClick();
        });
    }
    private void OnClick() {
        if(sceneInfo.money < itemData.prize){
            ShowAlertBuying();
            return;
        }

        if(!player) return;

        PlaySFX();
        sceneInfo.money -= itemData.prize;
        GameObject itemBuyed = Instantiate(collectible);
        player.collectibleItem = itemBuyed;
        player.PickUpItem();

        Destroy(itemBuyed);
    }
    private void ShowAlertBuying(){
        alert.SetActive(true);
    }
    private void PlaySFX(){
        if(!moneyClingSFX.isPlaying) moneyClingSFX.Play();
    }
}

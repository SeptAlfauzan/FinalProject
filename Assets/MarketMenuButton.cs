using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketMenuButton: MonoBehaviour
{
    public string itemName; 
    public string itemPrize;
    public ItemData itemData;
    public RawImage icon;
    public Texture texture;
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemPrizeText;
    public Player player;
    public GameObject collectible;
    void Start(){
        itemNameText.text = itemName;
        itemPrizeText.text = itemPrize;
        icon.texture = texture;

        this.GetComponent<Button>().onClick.AddListener(delegate {
            OnClick();
        });
    }
    private void OnClick() {
        Debug.Log("tes");
        if(!player) return;
        GameObject itemBuyed = Instantiate(collectible);
        player.collectibleItem = itemBuyed;
        player.PickUpItem();
        Destroy(itemBuyed);
    }
}

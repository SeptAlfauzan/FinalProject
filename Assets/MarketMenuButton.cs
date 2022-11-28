using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketMenuButton: MonoBehaviour
{
    public string itemName; 
    public string itemPrize;
    public ItemData itemData;
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemPrizeText;
    void Start()
    {
        itemNameText.text = itemName;
        itemPrizeText.text = itemPrize;
    }
}

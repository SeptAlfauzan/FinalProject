using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string nameItem;
    public ItemData itemData;

    public string GetName(){
        return nameItem;
    } 
}

using System.Collections;
using UnityEngine;
public class Planting : MonoBehaviour
{
    [SerializeField] private GameObject itemInHand;
    public GameObject GetItemInHand(){
        return itemInHand;
    }
    public void SetItemInHand(GameObject itemInHand){
        this.itemInHand = itemInHand;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollItem
{
    int length;
    ItemData itemData;
    public CollItem(int length, ItemData itemData){
        this.length = length;
        this.itemData = itemData;
    }
    public int GetLength(){
        return length;
    }
    public void SetLength(int num){
        length = num;
    }
    public ItemData GetItemData(){
        return itemData;
    }
    public void SetItemData(ItemData data){
        itemData = data;
    }
}


public class Player : MonoBehaviour {
    [SerializeField] float movementSpeed;
    // [SerializeField] bool isPickup = false;
    [SerializeField] bool isWalking = false;
    [SerializeField] float rotationSpeed;

    [SerializeField] Animator animator;
    float slowDown = 1;
    [SerializeField] private GameObject collectibleItem = null;
    [SerializeField] private Dictionary<string, CollItem> itemsInBag = new Dictionary<string, CollItem>();

    // Update is called once per frame
    private void Update() {
        isWalking = true;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if(Input.GetKey(KeyCode.LeftShift)) isWalking = false;
        slowDown = isWalking?  0.3f : 1;
        Vector3 movement = new Vector3(horizontal, 0, vertical) * Time.deltaTime * movementSpeed * slowDown;

        this.transform.Translate(movement, Space.World);
        AnimateRunOrWalk(movement.magnitude, isWalking);

        Vector3 normalizeMovement = movement.normalized;
        RotateBasedOnDirection(normalizeMovement);
//control mechanic
        if(Input.GetKey(KeyCode.E)) PickUpItem();
    }

    void RotateBasedOnDirection(Vector3 movementDirection){
        if(movementDirection != Vector3.zero){
            // transform.forward = movementDirection;
            Quaternion rotationTarget = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, rotationSpeed * Time.deltaTime);
        }
    }

    void AnimateRunOrWalk(float magnitude, bool isWalking){
        animator.SetBool("IsWalking", isWalking);
        animator.SetFloat("Speed", magnitude * 100);
    }


    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Collectible") collectibleItem = other.gameObject;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Collectible") collectibleItem = other.gameObject;
    }

    private void OnTriggerExit(Collider other) {    
        if(other.gameObject.tag == "Collectible") collectibleItem = null;
    }

    // interaction
    public void PickUpItem(){
        try
        {
            if(collectibleItem){
                
                string name = collectibleItem.GetComponent<Collectible>().GetName();
                int numberItem = itemsInBag.ContainsKey(name)? itemsInBag[name].GetLength() + 1 : 1;
                // int numberItem = 1;
                if(numberItem == 1){
                    itemsInBag.Add(name, new CollItem(numberItem, collectibleItem.GetComponent<Collectible>().itemData));
                }else{
                    itemsInBag[name].SetLength(numberItem);
                }
                // collectibleItem.SetActive(false);
                Destroy(collectibleItem);
                collectibleItem = null;
                
                Debug.Log(itemsInBag["a"].GetItemData());
            }
        }
        catch (System.Exception)
        {   
            throw;
        }
    }
}

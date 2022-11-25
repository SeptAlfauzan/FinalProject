using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingBin : MonoBehaviour
{
    [SerializeField] QuestData questData;
    [SerializeField] private Animator animator;
    private bool isPlayerNear = false;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.E) && isPlayerNear){
            DecreaseSelectedItem();
        } 
    }
    private void DecreaseSelectedItem(){
        animator.SetTrigger("Adding Item"); 
    }
    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Player") isPlayerNear = false;
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") isPlayerNear = true;    
    }
    private void OnCollisionStay(Collision other) {
        if(other.gameObject.tag == "Player") isPlayerNear = true;    
    }
}

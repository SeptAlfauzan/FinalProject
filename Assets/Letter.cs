using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    [SerializeField] bool isPlayerNear = false;
    [SerializeField] bool isGrabbed = false;
    [SerializeField] bool isOpened = false;
    [SerializeField] GameObject letter;

    private void Update() {
        if(isPlayerNear && Input.GetButtonDown("Interact") && !isGrabbed){
            letter.SetActive(true);
            isGrabbed = true;
            // Time.timeScale = 0;
        }else if(isPlayerNear && Input.GetButtonDown("Interact") && isGrabbed){
            letter.GetComponent<Animator>().SetTrigger("Open letter");
            isOpened = true;
        }else if(isPlayerNear && Input.GetButtonDown("Interact") && isOpened){
            isGrabbed = false;
            isOpened = false;
            letter.SetActive(false);
        }

        // if(isPlayerNear && Input.GetButtonDown("Interact") && isOpened){
        // }
    }
    public void CloseLetter(){
        isGrabbed = false;
        isOpened = false;
        letter.SetActive(false);
    }
    private void OnMouseDown() {
        if(isGrabbed){
            Debug.Log("asd");
            letter.GetComponent<Animator>().SetTrigger("Open letter");
            isOpened = true;
        }
        if(isOpened){
            isGrabbed = false;
            isOpened = false;
            letter.GetComponent<Animator>().SetTrigger("Close letter");
            letter.SetActive(false);
        } 
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") isPlayerNear = true;
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player") isPlayerNear = true;
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") isPlayerNear = false;
    }
}

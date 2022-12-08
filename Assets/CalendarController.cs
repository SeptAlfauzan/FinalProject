using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour
{
    [SerializeField] SceneInfo sceneInfo;
    [SerializeField] GameObject CalendarUI;
    [SerializeField] Text date;
    bool isPlayerInteract = false;
    private void Start() {
        
    }
    private void Update() {
        if(isPlayerInteract && Input.GetButton("Interact")) CalendarUI.SetActive(true);
        if(CalendarUI.isStatic && Input.GetButton("Interact")) CloseCalendar();
        date.text = sceneInfo.gameTime.ToString();
    }
    public void CloseCalendar(){
        CalendarUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            isPlayerInteract = true;
            other.gameObject.GetComponent<Player>().onArea = true;
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player"){
            isPlayerInteract = true;
            other.gameObject.GetComponent<Player>().onArea = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            isPlayerInteract = false;
            other.gameObject.GetComponent<Player>().onArea = false;
        }
    }
}

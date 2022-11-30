using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarController : MonoBehaviour
{
    [SerializeField] SceneInfo sceneInfo;
    [SerializeField] GameObject CalendarUI;
    bool isPlayerInteract = false;
    private void Update() {
        if(isPlayerInteract && Input.GetButton("Interact")) CalendarUI.SetActive(true);
        if(CalendarUI.isStatic && Input.GetButton("Interact")) CloseCalendar();
    }
    public void CloseCalendar(){
        CalendarUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") isPlayerInteract = true;
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player") isPlayerInteract = true;
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") isPlayerInteract = false;
    }
}

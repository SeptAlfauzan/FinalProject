using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] private string textDialog;
    [SerializeField] private DialogNpcController dialogNpcController;
    private bool isPlayerInteract = false;
    public GameObject player;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact") && isPlayerInteract) StartDialog();
    }
    private void StartDialog(){
        RotateToPlayer();

        
        dialogNpcController.StartDialog(textDialog);
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

    private void RotateToPlayer(){
        Quaternion _lookRotation;
        Vector3 _direction;
         //find the vector pointing from our position to the target
         _direction = (player.transform.position - transform.position).normalized;
         //create the rotation we need to be in to look at the target
         _lookRotation = Quaternion.LookRotation(_direction);
         //rotate us over time according to speed until we are in the required rotation
         transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 100);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Bed : MonoBehaviour
{
    [SerializeField] private SceneInfo sceneInfo;
    [SerializeField] private AlertController alertController;
    [SerializeField] private GameObject icon;
    [SerializeField] private LoadingScreen loadingScreen;
    private bool isNearBed = false;
    // Start is called before the first frame update
    private void LateUpdate() {
        if(isNearBed){
            icon.SetActive(true);
        }else{
            icon.SetActive(false);
        }
        if(Input.GetButton("Interact")){
            if(isNearBed){
                if(sceneInfo.dayTime >= 19 || sceneInfo.dayTime <= 2){
                    sceneInfo.gameTime += 1;
                    sceneInfo.dayTime = 6;
                    sceneInfo.playerStamina = 100;

                    loadingScreen.LoadScene("Home");
                }else{
                    Debug.Log("Masih terlalu dini untuk tidur");
                    alertController.gameObject.SetActive(true);
                    alertController.text = "Still too early to sleep.";
                }
            }
        } 
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            isNearBed = true;
            other.gameObject.GetComponent<Player>().onArea = true;
        }
    }
    private void OnCollisionStay(Collision other) {
        if(other.gameObject.tag == "Player"){
            isNearBed = true;
            other.gameObject.GetComponent<Player>().onArea = true;
        }
    }
    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Player"){
            isNearBed = false;
            other.gameObject.GetComponent<Player>().onArea = false;
        }
    }
}

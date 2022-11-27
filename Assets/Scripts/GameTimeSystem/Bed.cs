using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Bed : MonoBehaviour
{
    [SerializeField] private SceneInfo sceneInfo;
    private bool isNearBed = false;
    // Start is called before the first frame update
    private void LateUpdate() {
        if(Input.GetKey(KeyCode.E)){
            if(isNearBed){
                if(sceneInfo.dayTime >= 19 || sceneInfo.dayTime <= 1){
                    sceneInfo.gameTime += 1;
                    sceneInfo.dayTime = 6;
                    sceneInfo.playerStamina = 1;
                    SceneManager.LoadScene("Home");
                }else{
                    Debug.Log("Masih terlalu dini untuk tidur");
                }
            }else{
                Debug.Log("tidak didekat tempat tidur");
            }
        } 
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") isNearBed = true;
    }
    private void OnCollisionStay(Collision other) {
        if(other.gameObject.tag == "Player") isNearBed = true;
    }
    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Player") isNearBed = false;
    }
}

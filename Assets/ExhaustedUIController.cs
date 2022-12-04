using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ExhaustedUIController : MonoBehaviour
{
    [SerializeField] GameObject firstSelectedButton;
    [SerializeField] SceneInfo sceneInfo;
    [SerializeField] LoadingScreen loadingScreen;
    [SerializeField] Animator animator;
    
    [Header("Save Plant State Controller")]
    [SerializeField] SavePlantController savePlantController;
    private void Awake() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        animator.SetTrigger("Exhausted");
    }

    public void Continue(){
        Time.timeScale = 1f;
        
        sceneInfo.gameTime += 2;
        sceneInfo.dayTime = 6;
        sceneInfo.playerStamina = 70f;
        sceneInfo.lifePoint -= 1;

        try{
            if(SceneManager.GetActiveScene().name == "Farm") savePlantController.SaveCurrentData();
        } catch (System.Exception e){
            Debug.Log(e.Message);
        }

        loadingScreen.LoadScene("Home");
    }
}

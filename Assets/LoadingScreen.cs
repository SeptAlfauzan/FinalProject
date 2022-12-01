using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private GameObject loadingObj;

    public void LoadScene(string sceneId){
        StartCoroutine(LoadSceneAsync(sceneId));
    } 
    IEnumerator LoadSceneAsync(string sceneId){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingObj.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
}

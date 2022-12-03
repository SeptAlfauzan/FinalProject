using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource nightBGM;
    [SerializeField] AudioSource dayBGM;
    [SerializeField] AudioSource villageDayBGM;
    [SerializeField] AudioSource forestBGM;
    [SerializeField] AudioSource rainBGM;
    [SerializeField] SceneInfo sceneInfo;

    float initialVolume = 1;
    float muteVolume = 0;
    Coroutine currentCoroutine;
    AudioSource currentBgm;

    private void Start() {
     StopAllCoroutines();   
    }
    private void Update() {
        if(sceneInfo.isRain){
            if(currentBgm) currentBgm.Stop();
            StopAllCoroutines();
            if(!rainBGM.isPlaying) rainBGM.Play();
            return;
        }else{
            rainBGM.Stop();
        }
        if(sceneInfo.dayTime >= 19 || sceneInfo.dayTime <= 2){
            if(!nightBGM.isPlaying){
                currentBgm = nightBGM;
                nightBGM.volume = initialVolume;
                nightBGM.Play();
            }
        }else{
            if(nightBGM.volume <= initialVolume) currentCoroutine = StartCoroutine(FadeOutAudio(nightBGM, 1));
            
            if(nightBGM.volume == muteVolume + 0.05f){
                StopCoroutine(currentCoroutine);
                nightBGM.Stop();
            }
        }

        if(sceneInfo.dayTime >= 5 && sceneInfo.dayTime <= 17){
            if(SceneManager.GetActiveScene().name == "Village") currentBgm = villageDayBGM;
            if(SceneManager.GetActiveScene().name == "Forest") currentBgm = forestBGM;
            if(SceneManager.GetActiveScene().name == "Farm" || SceneManager.GetActiveScene().name == "Home") currentBgm = dayBGM;
            

            if(!currentBgm.isPlaying){
                currentBgm.volume = initialVolume;
                currentBgm.Play();
            }
        }else{
            if(currentBgm.volume == initialVolume) currentCoroutine = StartCoroutine(FadeOutAudio(currentBgm, 1));

            if(currentBgm.volume <= muteVolume + 0.05f){
                StopCoroutine(currentCoroutine);
                currentBgm.Stop();
            }
        }
        
    }

    private IEnumerator FadeOutAudio(AudioSource audio, float durationFadeOut){
        float currentDuration = 0f;
        do
        {
            audio.volume = Mathf.Lerp(initialVolume, muteVolume, currentDuration/durationFadeOut);
            currentDuration += Time.deltaTime;
            yield return null;
        } while (currentDuration <= durationFadeOut);
    }
}

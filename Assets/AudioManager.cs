using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource nightBGM;
    [SerializeField] AudioSource dayBGM;
    [SerializeField] AudioSource villageDayBGM;
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
            if(!villageDayBGM.isPlaying){
                currentBgm = villageDayBGM;
                villageDayBGM.volume = initialVolume;
                villageDayBGM.Play();
            }
        }else{
            if(villageDayBGM.volume == initialVolume) currentCoroutine = StartCoroutine(FadeOutAudio(villageDayBGM, 1));

            if(villageDayBGM.volume <= muteVolume + 0.05f){
                StopCoroutine(currentCoroutine);
                villageDayBGM.Stop();
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

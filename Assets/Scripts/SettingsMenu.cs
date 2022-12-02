using UnityEngine.Audio;
using UnityEngine;


public class SettingsMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject SettingMenuUI;

    private void Start() {
        Resume();
    }
    public AudioMixer mainMixer;
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume",volume);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        SettingMenuUI.SetActive(true);   
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void Resume()
    {
        SettingMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}

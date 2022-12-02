using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject SettingMenuUI;
    public AudioMixer mainMixer;
    [Header("Button Event System")]
    [SerializeField] GameObject firstButton;
    [SerializeField] GameObject previousCanvas;

    private void Awake() {
        Debug.Log("asdad");
    }
    private void Start() {
        Resume();
    }
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume",volume);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        SettingMenuUI.SetActive(true);
        // event system
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

    public void Back(){
        SettingMenuUI.SetActive(false);
        previousCanvas.SetActive(true);
    }
    private void OnEnable() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}

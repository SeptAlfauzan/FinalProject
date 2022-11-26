using UnityEngine;
using UnityEngine.UI;

public class StaminaBarController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SceneInfo sceneInfo;
    [SerializeField] private Slider staminaSlider; 
    void Start()
    {
        staminaSlider.value = sceneInfo.playerStamina;
    }
    // Update is called once per frame
    void Update()
    {
        staminaSlider.value = sceneInfo.playerStamina;
    }
}

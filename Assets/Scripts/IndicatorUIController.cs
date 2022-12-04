using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IndicatorUIController : MonoBehaviour
{
    [SerializeField] SceneInfo sceneInfo;
    [SerializeField] TextMeshProUGUI dayNameText;
    [SerializeField] TextMeshProUGUI dayTimeText;
    [SerializeField] TextMeshProUGUI moneyText;

    [Header("Icons")]
    [SerializeField] RawImage sun;
    [SerializeField] RawImage moon;

    private float alphaConst = 0.3f;
    private float normalConst = 1f;
    private List<string> dayNames = new List<string>(new string[] {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"});
    private void Update() {
        int dayIndex = sceneInfo.gameTime % 7;
        dayTimeText.text = Mathf.Floor(sceneInfo.dayTime) + ":00";
        dayNameText.text = dayNames[dayIndex];
        moneyText.text = sceneInfo.money.ToString();

        if(sceneInfo.dayTime >= 18 || sceneInfo.dayTime <= 2){
            Color sunCurrColor = sun.color;
            Color moonCurrColor = moon.color;
            sunCurrColor.a = alphaConst;
            moonCurrColor.a = normalConst;
    
            sun.color = sunCurrColor;
            moon.color = moonCurrColor;
        }else{
            Color sunCurrColor = sun.color;
            Color moonCurrColor = moon.color;
            sunCurrColor.a = normalConst;
            moonCurrColor.a = alphaConst;
    
            sun.color = sunCurrColor;
            moon.color = moonCurrColor;
        }
    }

}

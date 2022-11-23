using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IndicatorUIController : MonoBehaviour
{
    [SerializeField] SceneInfo sceneInfo;
    [SerializeField] TextMeshProUGUI dayNameText;
    [SerializeField] TextMeshProUGUI dayTimeText;
    [SerializeField] TextMeshProUGUI moneyText;
    private List<string> dayNames = new List<string>(new string[] {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"});
    private void Update() {
        int dayIndex = sceneInfo.gameTime % 7;
        dayTimeText.text = Mathf.Floor(sceneInfo.dayTime) + ":00";
        dayNameText.text = dayNames[dayIndex];
    }

}

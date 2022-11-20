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

    private void Update() {
        dayTimeText.text = Mathf.Floor(sceneInfo.dayTime) + ":00";
    }

}

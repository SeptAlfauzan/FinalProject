using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertController : MonoBehaviour
{
    [SerializeField] string text;
    [SerializeField] Text textObj;
    private void Start() {
        textObj.text = text;
    }
    public void Close(){
        this.gameObject.SetActive(false);
    }
}

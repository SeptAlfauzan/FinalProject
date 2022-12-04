using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertController : MonoBehaviour
{
    [SerializeField] public string text;
    [SerializeField] Text textObj;
    public GameObject closeButton;
    private void Start() {
        textObj.text = text;
        closeButton.GetComponent<Button>().onClick.AddListener(Close);
    }
    public void Close(){
        Debug.Log("tutup");
        this.gameObject.SetActive(false);
    }
}

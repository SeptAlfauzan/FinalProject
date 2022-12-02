using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ToolButton{
    public string nameTool;
    public GameObject gamePadButton;
    public GameObject keyboardButton;
}
public class ToolsSystem : MonoBehaviour
{
    public List<ToolButton> toolButtons;
    private bool isControllerConnected = false;
    // Update is called once per frame
    void Update()
    {
        bool currentControllerConnected = DetectIsControllerConnected();
        UpdateToolButtonsUI(currentControllerConnected);
        // if(isControllerConnected != currentControllerConnected){
        //     isControllerConnected = currentControllerConnected;
        // }
        Debug.Log(currentControllerConnected);
    }

    bool DetectIsControllerConnected(){
        try{
            return Input.GetJoystickNames()[0] == ""? false : true; 
        }
        catch (System.Exception)
        {
            return false;
        }
    }
    void UpdateToolButtonsUI(bool isJoyStickConnected){
        foreach (ToolButton toolButton in toolButtons){
            if(isJoyStickConnected){
                toolButton.keyboardButton.SetActive(false);
                toolButton.gamePadButton.SetActive(true);
            }else{
                toolButton.keyboardButton.SetActive(true);
                toolButton.gamePadButton.SetActive(false);
            }
        }
    }
}

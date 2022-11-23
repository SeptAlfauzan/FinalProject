using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolsSystem : MonoBehaviour
{
    private GameObject[] tools;
    private List<Toggle> toolButtons;
    [SerializeField] private int activeItem = 0;
    void Start()
    {
        tools = GameObject.FindGameObjectsWithTag("Tools");
        ActivateOneAndDisableRestButton(activeItem);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.C)) ActivateOneAndDisableRestButton(0);
        if(Input.GetKey(KeyCode.V)) ActivateOneAndDisableRestButton(1);
        if(Input.GetKey(KeyCode.B)) ActivateOneAndDisableRestButton(2);
    }

    void ActivateOneAndDisableRestButton(int index){
        Color gray;
        Color white;
        ColorUtility.TryParseHtmlString("#676767", out gray);
        ColorUtility.TryParseHtmlString("#ffffff", out white);
        activeItem = index;

        int i = 0;
        foreach (GameObject tool in tools){
            if(i == index){
                tool.GetComponent<Image>().color = Color.white;
            }else{
                tool.GetComponent<Image>().color = Color.gray;
            }
            i += 1;
        }
    }

    public int GetActiveItem(){
        return activeItem;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLamp : MonoBehaviour
{
    [SerializeField] SceneInfo sceneInfo;
    [SerializeField] GameObject lampCube;
    // Update is called once per frame
    void Update()
    {
        if(sceneInfo.dayTime >= 17 || sceneInfo.dayTime <= 2) lampCube.SetActive(true);
        else lampCube.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverUI;
    void Start()
    {
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    public void GameOverOn(){
        gameOverUI.SetActive(true);
    }
}

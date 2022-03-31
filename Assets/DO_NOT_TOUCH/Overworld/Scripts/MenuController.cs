using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject gameSelectScreen;
    public GameObject creditsScreen;


    public void Play(){
        gameSelectScreen.SetActive(true);
    }

    public void Credits(){
        creditsScreen.SetActive(true);
    }

    public void Close(){
        gameSelectScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }
}

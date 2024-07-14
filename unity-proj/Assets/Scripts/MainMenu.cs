using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickTheme1()
    {
        Debug.Log("onClick");
    }
    
    public void onPotraitButtonClick(PotraitCard potraitCard)
    {
        //SceneLoader.LoadInGameSceneWithPuzzleIndex(potraitCard.index);
        
        Debug.Log($"onPotraitButtonClick()->index:{potraitCard.index}");
    }
}

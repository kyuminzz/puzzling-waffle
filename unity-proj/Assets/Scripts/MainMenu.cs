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

    [SerializeField]
    GameObject[] pannels;

    public void onClickHome(GameObject targetPannel)
    {
        DeactiveAllPannels();
        targetPannel.SetActive(true);
        Debug.Log("onClickHome");
    }

    public void onClickMyInfo(GameObject targetPannel)
    {
        DeactiveAllPannels();
        Debug.Log("onClickMyInfo");
        targetPannel.SetActive(true);
    }
    public void onClickSetting(GameObject targetPannel)
    {
        DeactiveAllPannels();
        Debug.Log("onClickSetting");
        targetPannel.SetActive(true);
    }

    private void DeactiveAllPannels()
    {
        foreach (var pannel in pannels)
        {
            pannel.SetActive(false);
        }
    }
    
    public void onPotraitButtonClick(PotraitCard potraitCard)
    {
        //SceneLoader.LoadInGameSceneWithPuzzleIndex(potraitCard.index);
        
        Debug.Log($"onPotraitButtonClick()->index:{potraitCard.index}");
    }
}

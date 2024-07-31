using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject[] pannels;
    
    [SerializeField]
    GameObject MyInfoPanelObj;

    private void Awake()
    {
        Debug.Log("MainMenu Awake() 호출");
        MyInfoPanel.OnSwitchComplete = onClickCompleted;
        MyInfoPanel.OnSwitchInprogress = onClickInProgress;
    }

    public void onClickHome(GameObject targetPanel)
    {
        DeactiveAllPannels();
        targetPanel.SetActive(true);
        ScrollViewController.Instance.ShowAllStages();
        Debug.Log("onClickHome");
    }

    public void onClickMyInfo(GameObject targetPanel)
    {
        DeactiveAllPannels();
        Debug.Log("onClickMyInfo");
        targetPanel.SetActive(true);
        ScrollViewController.Instance.ShowCompletedPuzzles();
    }
    public void onClickCompleted()
    {
        DeactiveAllPannels();
        Debug.Log("onClickCompleted");
        MyInfoPanelObj.SetActive(true);
        ScrollViewController.Instance.ShowCompletedPuzzles();
    }
    public void onClickInProgress()
    {
        DeactiveAllPannels();
        Debug.Log("onClickMyInfo");
        MyInfoPanelObj.SetActive(true);
        ScrollViewController.Instance.ShowInProgressPuzzles();
    }
    public void onClickSetting(GameObject targetPanel)
    {
        DeactiveAllPannels();
        Debug.Log("onClickSetting");
        targetPanel.SetActive(true);
        ScrollViewController.Instance.Hide();
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

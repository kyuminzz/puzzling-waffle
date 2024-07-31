using UnityEngine;

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
    public void onClickCompleted(GameObject targetPanel)
    {
        DeactiveAllPannels();
        Debug.Log("onClickCompleted");
        targetPanel.SetActive(true);
        ScrollViewController.Instance.ShowCompletedPuzzles();
    }
    public void onClickInProgress(GameObject targetPanel)
    {
        DeactiveAllPannels();
        Debug.Log("onClickMyInfo");
        targetPanel.SetActive(true);
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

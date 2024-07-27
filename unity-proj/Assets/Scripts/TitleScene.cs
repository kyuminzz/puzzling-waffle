using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartApp();
    }

    private void StartApp()
    {
        LoadStageData();
    }

    private void LoadStageData()
    {
        StageDataManager.Instance.Initialize();

        SceneLoader.Instance.LoadMainMenuScene();
    }
    
}

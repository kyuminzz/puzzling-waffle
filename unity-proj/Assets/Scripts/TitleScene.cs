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
        Debug.Log("StartApp()->Start");
        
        LoadStageData();
        
        Debug.Log("StartApp()->End");
    }

    private void LoadStageData()
    {
        Debug.Log("LoadStageData(1)->");
        
        StageDataManager.Instance.Initialize();

        Debug.Log("LoadStageData(2)->");
        
        SceneLoader.Instance.LoadMainMenuScene();
        
        Debug.Log("LoadStageData(3)->");
    }
    
}

using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;


[System.Serializable]
public class Stage
{
    public int index;
    public List<string> tags;
}

[System.Serializable]
public class StageList
{
    public List<Stage> stages;
}

public class StageDataManager : MonoBehaviour
{
    private string stagesFileName = "stages";

    void Start()
    {
        StageList stageList = LoadStagesFromResources();
        // foreach (var stage in stageList.stages)
        // {
        //     Debug.Log($"Stage {stage.index}: {string.Join(", ", stage.tags)}");
        // }
    }

    // Resources 폴더에서 JSON 파일을 로드
    public StageList LoadStagesFromResources()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(stagesFileName);
        if (jsonFile != null)
        {
            return JsonConvert.DeserializeObject<StageList>(jsonFile.text);
        }
        else
        {
            Debug.LogWarning("Stages file not found in Resources, returning default StageList.");
            return new StageList { stages = new List<Stage>() };
        }
    }
}
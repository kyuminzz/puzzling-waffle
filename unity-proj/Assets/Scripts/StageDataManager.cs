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
    private static StageDataManager _instance;
    public static StageDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<StageDataManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("StageDataManager");
                    _instance = go.AddComponent<StageDataManager>();
                }
            }
            return _instance;
        }
    }
    
    private string stagesFileName = "stages";
    private StageList _stageList;
    private List<string> activeTags = new List<string>();
    private Queue<Stage> activeStagesQueue = new Queue<Stage>();

    public StageList stageList
    {
        get
        {
            if (_stageList == null)
            {
                _stageList = LoadStagesFromResources();
            }

            return _stageList;
        }
    }
    List<string> tagNames = new List<string>(){"ANIME", "GIRL", "FANTASY", "CUTE", "LOVE", "DARK", "MANGA", "ART", "SKETCH", "COMIC"};

    public List<string> AllTags => tagNames;
    
    // 활성화된 태그를 기반으로 active stage를 갱신
    public void UpdateActiveStages()
    {
        activeStagesQueue.Clear();
        StageList filteredStages = LoadStagesFromTag(activeTags);
        
        List<Stage> shuffledStages = new List<Stage>(filteredStages.stages);
        Shuffle(shuffledStages);

        foreach (var stage in shuffledStages)
        {
            activeStagesQueue.Enqueue(stage);
        }
    }

    private void Shuffle(List<Stage> stages)
    {
        for (int i = 0; i < stages.Count; i++)
        {
            Stage temp = stages[i];
            int randomIndex = Random.Range(0, stages.Count);
            stages[i] = stages[randomIndex];
            stages[randomIndex] = temp;
        }
    }
    
    
    public void AddActiveTag(string tag)
    {
        if (!activeTags.Contains(tag))
        {
            activeTags.Add(tag);
            Debug.Log($"{tag} 활성화됨");
        }

        UpdateActiveStages();
    }
    public void RemoveActiveTag(string tag)
    {
        if (activeTags.Contains(tag))
        {
            activeTags.Remove(tag);
            Debug.Log($"{tag} 비활성화됨");
        }

        UpdateActiveStages();
    }
    public List<string> GetActiveTags()
    {
        return activeTags;
    }

    void Start()
    {
        // if (_stageList == null)
        // {
        //     _stageList = LoadStagesFromResources();
        // }
        // foreach (var stage in stageList.stages)
        // {
        //     Debug.Log($"Stage {stage.index}: {string.Join(", ", stage.tags)}");
        // }
    }

    // Resources 폴더에서 JSON 파일을 로드
    private StageList LoadStagesFromResources()
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

    /// <summary>
    /// 태그를 이용하여 스테이지를 로드
    /// 스테이지의 태그가 하나라도 포함된 스테이지 리스트를 반환
    /// </summary>
    /// <param name="tags"></param>
    /// <returns></returns>
    //public StageList LoadStagesFromTag(string[] tags)
    public StageList LoadStagesFromTag(List<string> tags)
    {
        StageList filteredStages = new StageList { stages = new List<Stage>() };
        
        if (tags.Count == 0)
        {
            filteredStages.stages.AddRange(stageList.stages);
            return filteredStages;
        }
        
        foreach (var stage in stageList.stages)
        {
            foreach (var tag in tags)
            {
                if (stage.tags.Exists((t) => t.ToLower() == tag.ToLower()))
                {
                    filteredStages.stages.Add(stage);
                    break; // 태그 하나라도 포함되면 추가하고 다음 스테이지로 넘어감
                }
            }
        }

        return filteredStages;
    }
    
    // active stage에서 정해진 수량만큼 꺼내서 리스트 형태로 반환
    public List<Stage> GetActiveStages(int count)
    {
        List<Stage> stages = new List<Stage>();
        for (int i = 0; i < count && activeStagesQueue.Count > 0; i++)
        {
            stages.Add(activeStagesQueue.Dequeue());
        }
        return stages;
    }
}
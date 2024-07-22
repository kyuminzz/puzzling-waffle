using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Random = UnityEngine.Random;


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
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }
    
    private string stagesFileName = "stages";
    private StageList _allStageList;
    private List<string> activeTags = new List<string>();
    private Queue<Stage> activeStagesQueue = new Queue<Stage>();
    private Queue<Stage> completeStagesQueue = new Queue<Stage>();
    private Queue<Stage> inProgressStagesQueue = new Queue<Stage>();
    //List<string> tagNames = new List<string>(){"ANIME", "GIRL", "FANTASY", "CUTE", "LOVE", "DARK", "MANGA", "ART", "SKETCH", "COMIC"};
    List<string> tagNames = new List<string>();
    private StageList allStageList => _allStageList;
    public List<string> AllTags => tagNames;
    
    public void Initialize()
    {
        _allStageList = LoadStagesFromResources();
            
        SetAllTags();
    }

    public bool IsActiveTag(string tag)
    {
        return activeTags.Contains(tag);
    }

    private void SetAllTags()
    {
        foreach (var stage in _allStageList.stages)
        {
            foreach (var tag in stage.tags)
            {
                if (!tagNames.Contains(tag))
                {
                    tagNames.Add(tag);
                }
            }
        }
    }
    
    

    // 활성화된 태그를 기반으로 active stage를 갱신
    public void UpdateActiveStages()
    {
        activeStagesQueue.Clear();
        StageList filteredStages = LoadStagesFromTag(activeTags);
        
        List<Stage> shuffledStages = new List<Stage>(filteredStages.stages);
        Shuffle(shuffledStages);

        foreach (var stage in shuffledStages)
        {
            if(stage.index % 2 == 0)
                completeStagesQueue.Enqueue(stage);
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
        //todo: tag 정보가 갱신 될 때 사용자 태그 정보를 로컬에 저장해뒀다가
        //todo: 다음에 앱을 실행할 때 불러와서 사용자가 선택한 태그를 불러오도록 수정
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


    // Resources 폴더에서 JSON 파일을 로드
    private StageList LoadStagesFromResources()
    {
        Debug.Log("LoadStagesFromResources(1)->");
        
        TextAsset jsonFile = Resources.Load<TextAsset>(stagesFileName);
        if (jsonFile != null)
        {
            Debug.Log("LoadStagesFromResources(2)->");
            return JsonConvert.DeserializeObject<StageList>(jsonFile.text);
        }
        else
        {
            Debug.LogWarning("Stages file not found in Resources, returning default StageList.");
            return new StageList { stages = new List<Stage>() };
        }
    }

    /// <summary>
    /// 스테이지의 태그가 하나라도 포함된 스테이지 리스트를 반환
    /// </summary>
    /// <param name="tags"></param>
    /// <returns></returns>
    public StageList LoadStagesFromTag(List<string> tags)
    {
        StageList filteredStages = new StageList { stages = new List<Stage>() };
        
        if (tags.Count == 0)
        {
            filteredStages.stages.AddRange(allStageList.stages);
            return filteredStages;
        }
        
        foreach (var stage in allStageList.stages)
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
    private List<Stage> GetStages(int count, Func<Queue<Stage>> queueProvider)
    {
        List<Stage> stages = new List<Stage>();
        Queue<Stage> queue = queueProvider();

        for (int i = 0; i < count && queue.Count > 0; i++)
        {
            stages.Add(queue.Dequeue());
        }

        return stages;
    }

    public List<Stage> GetActiveStages(int count)
    {
        return GetStages(count, () => activeStagesQueue);
    }

    public List<Stage> GetCompleteStages(int count)
    {
        return GetStages(count, () => completeStagesQueue);
    }

    public List<Stage> GetInProgressStages(int count)
    {
        return GetStages(count, () => inProgressStagesQueue);
    }
}
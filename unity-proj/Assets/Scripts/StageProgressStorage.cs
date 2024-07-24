using UnityEngine;
using Newtonsoft.Json;

public class StageProgressStorage : MonoBehaviour
{
    private static StageProgressStorage _instance;
    public static StageProgressStorage Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<StageProgressStorage>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("StageProgressStorage");
                    _instance = go.AddComponent<StageProgressStorage>();
                    DontDestroyOnLoad(go); // 씬 전환 시 파괴되지 않도록 설정
                }
                else if (_instance != FindObjectOfType<StageProgressStorage>())
                {
                    Destroy(FindObjectOfType<StageProgressStorage>().gameObject); // 중복 방지
                }
            }
            return _instance;
        }
    }
    
    private string _stageProgressDataPath;

    private string stageProgressDataPath
    {
        get
        {
            if(string.IsNullOrEmpty(_stageProgressDataPath))
                _stageProgressDataPath = System.IO.Path.Combine(Application.persistentDataPath, "stage_progress_data.json");
            return _stageProgressDataPath;
        }
    }
    
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 파괴
        }
    }
    void Start()
    {
    }
    
    public void ClearData()
    {
        if (System.IO.File.Exists(stageProgressDataPath))
        {
            System.IO.File.Delete(stageProgressDataPath);
        }
    }

    public void SaveData(StageProgressData stageProgressData)
    {
        string json = JsonConvert.SerializeObject(stageProgressData, Formatting.Indented);
        System.IO.File.WriteAllText(stageProgressDataPath, json);
    }

    public StageProgressData LoadData()
    {
        if (System.IO.File.Exists(stageProgressDataPath))
        {
            string json = System.IO.File.ReadAllText(stageProgressDataPath);
            return JsonConvert.DeserializeObject<StageProgressData>(json);
        }
        else
        {
            return new StageProgressData(); // 파일이 없으면 기본 값 반환
        }
    }
}
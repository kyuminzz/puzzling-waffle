using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _instance;
    public static SceneLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneLoader>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("SceneLoader");
                    _instance = go.AddComponent<SceneLoader>();
                }
            }
            return _instance;
        }
    }
    
    public static int puzzleIndex;
    
    void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 유지
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 제거
        }
    }
    private void OnEnable()
    {
        // 씬 로드 이벤트에 메서드 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 씬 로드 이벤트에서 메서드 등록 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 로드될 때 호출되는 메서드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}, puzzleIndex:{puzzleIndex}");

        if (scene.name == "InGameScene")
        {
            PuzzleManager.Instance.LoadPuzzle(puzzleIndex);
        }
    }
    
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainUIScene");
    }
    public void LoadInGameSceneWithPuzzleIndex(int index)
    {
        puzzleIndex = index;

        SceneManager.LoadScene("InGameScene");
    }
}
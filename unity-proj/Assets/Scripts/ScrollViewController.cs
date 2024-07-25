using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScrollViewController : MonoBehaviour
{
    private static ScrollViewController _instance;
    public static ScrollViewController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScrollViewController>();
                if (_instance == null)
                {
                    Debug.LogError("ScrollViewController is not found in the scene. Creating a new instance.");
                    GameObject go = new GameObject("ScrollViewController");
                    _instance = go.AddComponent<ScrollViewController>();
                }
            }
            return _instance;
        }
    }
    
    
    enum EScrollViewState
    {
        None,
        AllStages,
        InProgress,
        Completed
    }
    private EScrollViewState scrollViewState = EScrollViewState.None;
    
    private ScrollRect scrollRect;

    public GameObject uiPrefab;
    public GameObject moreButtonPrefab;
    private RectTransform moreButtonRect;
    public float space = 50f;
    public float horizontalSpace = 20f;
    public List<RectTransform> uiObjects = new List<RectTransform>();
    private const int IMAGES_TO_LOAD = 8;
    float y = 0f;
    float maxRowHeight = 0f;
    //private List<int> imageIndices;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        LoadAllStages();
    }
    
    public void ShowAllStages()
    {
        Debug.Log("ShowAllStages");
        
        scrollViewState = EScrollViewState.AllStages;
        
        gameObject.SetActive(true);
        
        RefreshPuzzleList();
        
        LoadAllStages();
    }

    public void ShowCompletedPuzzles()
    {
        Debug.Log("ShowCompletedPuzzles");

        scrollViewState = EScrollViewState.Completed;
        
        RefreshPuzzleList();
        
        LoadCompleteImages();
    }
    public void ShowInProgressPuzzles()
    {
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void RefreshPuzzleList()
    {
        foreach (var uiObject in uiObjects)
        {
            uiObject.gameObject.SetActive(false);
        }
        y = 0f;
        maxRowHeight = 0f;
    }
    
    public void LoadAllStages()
    {
        StageDataManager.Instance.UpdateActiveStages();
        LoadImagesFromStageData(IMAGES_TO_LOAD, StageDataManager.Instance.GetActiveStages);
        AddMoreButton();
    }

    void LoadCompleteImages()
    {
        StageDataManager.Instance.UpdateCompleteStages();//todo:임시 로직. 추후에 수정 필요
        LoadImagesFromStageData(IMAGES_TO_LOAD, StageDataManager.Instance.GetCompleteStages);
        AddMoreButton();
    }
    
    void LoadMoreImages()
    {
        Debug.Log("LoadMoreImages");
        y -= moreButtonRect.rect.height - space;
        switch (scrollViewState)
        {
            case EScrollViewState.AllStages:
                LoadImagesFromStageData(IMAGES_TO_LOAD, StageDataManager.Instance.GetActiveStages);
                break;
            case EScrollViewState.Completed:
                LoadImagesFromStageData(IMAGES_TO_LOAD, StageDataManager.Instance.GetCompleteStages);
                break;
            default:
                Debug.LogError($"Invalid state : {scrollViewState}");
                break;
        }
        
        AddMoreButton();
    }
    
    void LoadImagesFromStageData(int count, Func<int, List<Stage>> stageAction)
    {
        //var indexIndex = StageDataManager.Instance.PopStages();
        var indexIndex = stageAction.Invoke(IMAGES_TO_LOAD);

        for (int i = 0; i < indexIndex.Count; i++)
        {
            int index = indexIndex[i].index;
            int seq = i + 1;
            Sprite sprite = SpriteManager.Instance.GetSpriteByIndex(index);

            if (sprite == null)
                continue;

            RectTransform newUi = GetOrAddNewUIObject(sprite);
            PotraitCard potraitCard = newUi.GetComponent<PotraitCard>();
            Button btnCard = potraitCard.GetComponent<Button>();
            btnCard.onClick.AddListener(() => {
                Debug.Log($"Clicked on card {potraitCard.index}");
                SceneLoader.Instance.LoadInGameSceneWithPuzzleIndex(potraitCard.index);
            });
            potraitCard.index = index;

            float x = (seq % 2 == 1)
                ? -newUi.rect.width / 2 - horizontalSpace / 2
                : newUi.rect.width / 2 + horizontalSpace / 2;

            newUi.anchoredPosition = new Vector2(x, -y - newUi.rect.height / 2);

            maxRowHeight = Mathf.Max(maxRowHeight, newUi.rect.height);

            if (seq % 2 == 0 && i != count - 1)
            {
                y += maxRowHeight + space;
                Debug.Log($"Loading y : {y}, portrait height{newUi.rect.height}");
                maxRowHeight = 0f;
            }

            newUi.gameObject.SetActive(true);
        }

        y += maxRowHeight;
    }
    void AddMoreButton()
    {
        if (moreButtonRect == null)
        {
            moreButtonRect = Instantiate(moreButtonPrefab, scrollRect.content).GetComponent<RectTransform>();
            Button button = moreButtonRect.GetComponent<Button>();
            button.onClick.AddListener(LoadMoreImages);
            uiObjects.Add(moreButtonRect);
        }

        moreButtonRect.anchoredPosition = new Vector2(0, -y - moreButtonRect.rect.height / 2);
        
        y += moreButtonRect.rect.height;
        moreButtonRect.gameObject.SetActive(true);
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, y);
    }
    
    public RectTransform AddNewUIObject(Sprite sprite)
    {
        var newUi = Instantiate(uiPrefab, scrollRect.content).GetComponent<RectTransform>();
        Image image = newUi.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = sprite;
        }
        uiObjects.Add(newUi);
        
        return newUi;
    }
    public RectTransform GetOrAddNewUIObject(Sprite sprite)
    {
        RectTransform newUi = uiObjects.Find(uiObject => !uiObject.gameObject.activeSelf);

        if (newUi == null)
        {
            newUi = Instantiate(uiPrefab, scrollRect.content).GetComponent<RectTransform>();
            uiObjects.Add(newUi);
        }

        Image image = newUi.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = sprite;
        }

        return newUi;
    }
}
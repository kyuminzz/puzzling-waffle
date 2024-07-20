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
    private List<int> imageIndices;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        LoadImages();
    }

    private void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    
    void LoadMoreImages()
    {
        Debug.Log("LoadMoreImages");
        y -= moreButtonRect.rect.height - space;
        LoadImagesFromIndices(IMAGES_TO_LOAD);
        AddMoreButton();
    }
    
    void LoadImagesFromIndices(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int index = imageIndices[i];
            int seq = i + 1;
            Sprite sprite = SpriteManager.Instance.GetSpriteByIndex(index);

            if (sprite == null)
                continue;

            RectTransform newUi = AddNewUIObject(sprite);
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

            uiObjects.Add(newUi);
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

        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, y);
    }
    

    void LoadImages()
    {
        Debug.Log("Starting to load images");

        imageIndices = new List<int>();
        for (int i = 1; i <= SpriteManager.IMAGE_COUNT; i++)
        {
            imageIndices.Add(i);
        }

        Shuffle(imageIndices);

        LoadImagesFromIndices(IMAGES_TO_LOAD);
        AddMoreButton();
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
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScrollViewController : MonoBehaviour
{
    private ScrollRect scrollRect;

    public GameObject uiPrefab;
    public float space = 50f;
    public float horizontalSpace = 20f;
    public List<RectTransform> uiObjects = new List<RectTransform>();
    
    //private const string IMAGE_PATH = "Assets/images/potrait/";
    private const string IMAGE_PATH = "images/potrait/";
    private const string IMAGE_PREFIX = "puzzle_potrait_";
    private const int IMAGE_COUNT = 30;

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
    
    void LoadImages()
    {
        Debug.Log("Starting to load images");
    
        float y = 0f;
        float maxRowHeight = 0f;
        
        // 1부터 IMAGE_COUNT까지의 숫자를 리스트에 담기
        List<int> imageIndices = new List<int>();
        for (int i = 1; i <= IMAGE_COUNT; i++)
        {
            imageIndices.Add(i);
        }

        // 리스트를 랜덤하게 셔플하기
        Shuffle(imageIndices);

        for (int i = 0; i < IMAGE_COUNT; i++)
        //foreach (int index in imageIndices)
        {
            int index = imageIndices[i];
            int seq = i + 1;
            //string imagePath = $"{IMAGE_PATH}{IMAGE_PREFIX}{i:D3}{IMAGE_SUFFIX}";
            string imagePath = $"{IMAGE_PATH}{IMAGE_PREFIX}{index:D4}";
            Sprite sprite = Resources.Load<Sprite>(imagePath);
            if (sprite == null)
            {
                Debug.Log($"Sprite is null. path : {imagePath}");
                continue;
            }

            RectTransform newUi = AddNewUIObject(sprite);

            float x = (seq % 2 == 1)
                ? -newUi.rect.width / 2 - horizontalSpace / 2
                : newUi.rect.width / 2 + horizontalSpace / 2;

            newUi.anchoredPosition = new Vector2(x, -y - newUi.rect.height / 2);

            maxRowHeight = Mathf.Max(maxRowHeight, newUi.rect.height);

            //if (seq % 2 == 0 || seq == IMAGE_COUNT)
            if (seq % 2 == 0)
            {
                y += maxRowHeight + space;
                maxRowHeight = 0f;
            }
        }

        // 모든 UI 객체를 포함할 수 있도록 콘텐츠 크기 설정
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, y);
    }
    
    Sprite LoadSprite(string path)
    {
        if (File.Exists(path))
        {
            byte[] fileData = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        }
        Debug.LogWarning($"Image not found at path: {path}");
        return null;
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

        // float y = 0f;
        // for (int i = 0; i < uiObjects.Count; i++)
        // {
        //     uiObjects[i].anchoredPosition = new Vector2(0f, -y);
        //     y += uiObjects[i].sizeDelta.y + space;
        // }

        //scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, y);

        return newUi;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TagScrollViewController : MonoBehaviour
{
    private ScrollRect scrollRect;
    public GameObject uiPrefab;
    public float horizontalSpace = 20f; // 수평 간격을 의미하도록 변수명 변경
    public List<RectTransform> uiObjects = new List<RectTransform>();
    
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        LoadTags();
    }

    void LoadTags()
    {
        Debug.Log("Starting to load tags");

        var tagNames = new List<string>(){"ANIME", "GIRL", "FANTASY", "CUTE", "LOVE", "DARK", "MANGA", "ART", "SKETCH", "COMIC"};
        foreach (var tagName in tagNames)
        {
            AddNewUIObject(tagName);
        }
    }

    public RectTransform AddNewUIObject(string tagName)
    {
        var newUi = Instantiate(uiPrefab, scrollRect.content).GetComponent<RectTransform>();
        var swithToggle = newUi.GetComponent<SwitchToggle>();
        swithToggle.SetOnText(tagName);
        float x = 0f;
        if (uiObjects.Count > 0)
        {
            RectTransform lastUi = uiObjects[uiObjects.Count - 1];
            x = lastUi.anchoredPosition.x + lastUi.sizeDelta.x + horizontalSpace;
        }
        
        newUi.anchoredPosition = new Vector2(x, 0f);
        uiObjects.Add(newUi);

        // 콘텐츠 크기 조정
        float contentWidth = x + newUi.sizeDelta.x;
        scrollRect.content.sizeDelta = new Vector2(contentWidth, scrollRect.content.sizeDelta.y);

        return newUi;
    }
}

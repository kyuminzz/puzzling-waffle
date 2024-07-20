using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TagScrollViewController : MonoBehaviour
{
    private List<SwitchToggle> toggles = new List<SwitchToggle>();
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

        StageDataManager.Instance.AllTags.ForEach(tag =>
        {
            AddNewUIObject(tag);
        });
    }
    
    void OnToggleChanged(SwitchToggle toggle, bool isActive)
    {
        var tag = toggle.PuzzleTag;
        
        if (isActive)
        {
            StageDataManager.Instance.AddActiveTag(tag);
        }
        else
        {
            StageDataManager.Instance.RemoveActiveTag(tag);
        }
    }

    public RectTransform AddNewUIObject(string tagName)
    {
        var newUi = Instantiate(uiPrefab, scrollRect.content).GetComponent<RectTransform>();
        var swithToggle = newUi.GetComponent<SwitchToggle>();
        
        swithToggle.PuzzleTag = tagName;
        swithToggle.SetOnText(tagName);
        swithToggle.toggle.onValueChanged.AddListener((isActive) =>OnToggleChanged(swithToggle, isActive));
        toggles.Add(swithToggle);
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

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
        Debug.Log("TagScrollViewController Start(1)");
        scrollRect = GetComponent<ScrollRect>();
        Debug.Log("TagScrollViewController Start(2)");
        LoadTags();
        Debug.Log("TagScrollViewController Start(3)");
    }

    private void OnEnable()
    {
        Debug.Log("TagScrollViewController OnEnable()");
    }
    void LoadTags()
    {
        Debug.Log($"LoadTags(1)->Count:{StageDataManager.Instance.AllTags.Count}");

        StageDataManager.Instance.AllTags.ForEach(tag =>
        {
            Debug.Log($"LoadTags(3)->");
            AddNewUIObject(tag);
        });
        Debug.Log($"LoadTags(2)->");
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
        ScrollViewController.Instance.RefreshPuzzleList();
    }

    public RectTransform AddNewUIObject(string tagName)
    {
        Debug.Log($"AddNewUIObject(1)->");
        var newUi = Instantiate(uiPrefab, scrollRect.content).GetComponent<RectTransform>();
        var swithToggle = newUi.GetComponent<SwitchToggle>();
        
        swithToggle.PuzzleTag = tagName;
        Debug.Log($"AddNewUIObject(2)->");
        if(StageDataManager.Instance.IsActiveTag(tagName))
            swithToggle.toggle.isOn = true;
        Debug.Log($"AddNewUIObject(3)->");
        swithToggle.SetOnText(tagName);
        swithToggle.toggle.onValueChanged.AddListener((isActive) =>OnToggleChanged(swithToggle, isActive));
        Debug.Log($"AddNewUIObject(4)->");
        toggles.Add(swithToggle);
        float x = 0f;
        if (uiObjects.Count > 0)
        {
            RectTransform lastUi = uiObjects[uiObjects.Count - 1];
            x = lastUi.anchoredPosition.x + lastUi.sizeDelta.x + horizontalSpace;
        }
        Debug.Log($"AddNewUIObject(5)->");
        newUi.anchoredPosition = new Vector2(x, 0f);
        uiObjects.Add(newUi);
        Debug.Log($"AddNewUIObject(6)->");
        // 콘텐츠 크기 조정
        float contentWidth = x + newUi.sizeDelta.x;
        scrollRect.content.sizeDelta = new Vector2(contentWidth, scrollRect.content.sizeDelta.y);
        Debug.Log($"AddNewUIObject(7)->");
        return newUi;
    }
}

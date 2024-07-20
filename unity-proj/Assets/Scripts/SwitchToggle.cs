using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] private RectTransform uiHandleRectTransform;
    [SerializeField] private RectTransform uiBackgroundRectTransform;
    [SerializeField] private Color backgroundActiveColor;
    [SerializeField] private Color handleActiveColor;
    [SerializeField] private TMP_Text onText;
    Image backgroundDefaultImage, handleDefaultImage;
    Color backgroundDefaultColor, handleDefaultColor;
    public Toggle toggle;

    private Vector2 handlePosition;
    public string PuzzleTag { get; set; }

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        
        if (uiHandleRectTransform != null)
        {
            handlePosition = uiHandleRectTransform.anchoredPosition;
            handleDefaultImage = uiHandleRectTransform.GetComponent<Image>();
        }
        
        backgroundDefaultImage = uiBackgroundRectTransform.GetComponent<Image>();
        
        backgroundDefaultColor = backgroundDefaultImage.color;
        
        if (handleDefaultImage != null)
            handleDefaultColor = handleDefaultImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);
    }

    public void SetOnText(string text)
    {
        onText.text = text;
    }
    
    void OnSwitch(bool isOn)
    {
        backgroundDefaultImage.color = isOn ? backgroundActiveColor : backgroundDefaultColor;

        if(uiHandleRectTransform != null)
            uiHandleRectTransform.anchoredPosition = isOn ? handlePosition * -1 : handlePosition;
        
        if (handleDefaultImage != null)
            handleDefaultImage.color = isOn ? handleActiveColor : handleDefaultColor;
    }

    void OnDestory()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
    
}

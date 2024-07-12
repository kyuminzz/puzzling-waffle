using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwitchToggle : MonoBehaviour
{
    [SerializeField] private RectTransform uiHandleRectTransform;
    [SerializeField] private Color backgroundActiveColor;
    [SerializeField] private Color handleActiveColor;
    Image backgroundDefaultImage, handleDefaultImage;
    Color backgroundDefaultColor, handleDefaultColor;
    private Toggle toggle;

    private Vector2 handlePosition;
    
    void Awake()
    {
        toggle = GetComponent<Toggle>();
        handlePosition = uiHandleRectTransform.anchoredPosition;
        backgroundDefaultImage = uiHandleRectTransform.parent.GetComponent<Image>();
        handleDefaultImage = uiHandleRectTransform.GetComponent<Image>();
        
        backgroundDefaultColor = backgroundDefaultImage.color;
        handleDefaultColor = handleDefaultImage.color;
        
        toggle.onValueChanged.AddListener(OnSwitch);
    }

    void OnSwitch(bool isOn)
    {
        uiHandleRectTransform.anchoredPosition = isOn ? handlePosition * -1 : handlePosition;
        backgroundDefaultImage.color = isOn ? backgroundActiveColor : backgroundDefaultColor;
        handleDefaultImage.color = isOn ? handleActiveColor : handleDefaultColor;
    }
    
    void OnDestory()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
    
}

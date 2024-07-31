using System;
using UnityEngine;
using UnityEngine.UI;

public class MyInfoPanel : MonoBehaviour
{
    [SerializeField]
    private SwitchToggle switchToggleComplete;
    
    [SerializeField]
    private SwitchToggle switchToggleInprogress;
    public static Action OnSwitchComplete;
    public static Action OnSwitchInprogress;

    private void Start()
    {
        switchToggleComplete.toggle.onValueChanged.AddListener(SwitchComplete);
        switchToggleInprogress.toggle.onValueChanged.AddListener(SwitchInprogress);
    }

    private void SwitchComplete(bool isOn)
    {
        Debug.Log("OnSwitchComplete");

        if (isOn)
        {
            SetToggleState(switchToggleComplete, false, true);
            SetToggleState(switchToggleInprogress, true, false);
            OnSwitchComplete?.Invoke();
        }
        
    }

    private void SwitchInprogress(bool isOn)
    {
        Debug.Log("OnSwitchInprogress");

        if (isOn)
        {
            SetToggleState(switchToggleInprogress, false, true);
            SetToggleState(switchToggleComplete, true, false);
            OnSwitchInprogress?.Invoke();
        }
    }

    private void SetToggleState(SwitchToggle switchToggle, bool interactable, bool? isOn = null)
    {
        // 이벤트를 일시적으로 제거하여 무한 루프를 방지
        switchToggleComplete.toggle.onValueChanged.RemoveListener(SwitchComplete);
        switchToggleInprogress.toggle.onValueChanged.RemoveListener(SwitchInprogress);

        if (isOn.HasValue)
        {
            switchToggle.toggle.isOn = isOn.Value;
        }
        
        switchToggle.toggle.interactable = interactable;

        // 이벤트 다시 추가
        switchToggleComplete.toggle.onValueChanged.AddListener(SwitchComplete);
        switchToggleInprogress.toggle.onValueChanged.AddListener(SwitchInprogress);
    }
}
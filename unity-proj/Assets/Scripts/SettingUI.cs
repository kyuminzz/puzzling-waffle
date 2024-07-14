using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingUI : MonoBehaviour
{
    [SerializeField] private Button Beginner4;
    [SerializeField] private Button Beginner5;

    private Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        Beginner4.onClick.AddListener(() => SetDifficulty(EPuzzleDifficulty.Beginner4));
        Beginner5.onClick.AddListener(() => SetDifficulty(EPuzzleDifficulty.Beginner5));
    }
    void OnEnable()
    {
        RefreshButtons();
        
        animator.SetTrigger("Open");
    }

    private void RefreshButtons()
    {
        switch (PlayerSettings.puzzleDifficulty)
        {
            case EPuzzleDifficulty.Beginner4:
                //SetDifficulty(PuzzleDifficulty.Beginner4);
                Beginner4.image.color = Color.yellow;
                Beginner5.image.color = Color.white;
                break;
            case EPuzzleDifficulty.Beginner5:
                //SetDifficulty(EPuzzleDifficulty.Beginner5);
                Beginner4.image.color = Color.white;
                Beginner5.image.color = Color.yellow;
                break;
        }
    }
    
    public void SetDifficulty(EPuzzleDifficulty difficulty)
    {
        PlayerSettings.puzzleDifficulty = difficulty;
    }
    
    public void OnButtonClick(int puzzleDifficulty)
    {
        SetDifficulty((EPuzzleDifficulty)puzzleDifficulty);

        RefreshButtons();
    }
    
    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }
    public IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        animator.ResetTrigger("Close");
    }
}

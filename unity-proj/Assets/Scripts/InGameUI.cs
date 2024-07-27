using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public PuzzleManager puzzleManager; // PuzzleManager를 참조할 변수
    
    [SerializeField]
    private Button changeSpriteButton; // 스프라이트 변경 버튼

    [SerializeField]
    private GameObject clearPopup; // 클리어 팝업

    private void Awake()
    {
        PuzzleManager.OnClearPuzzle += OnClearPuzzle;
        PuzzleManager.OnPuzzlePiecePlaced += OnPuzzlePiecePlaced;
    }
    
    private void OnDestroy()
    {
        PuzzleManager.OnClearPuzzle -= OnClearPuzzle;
        PuzzleManager.OnPuzzlePiecePlaced -= OnPuzzlePiecePlaced;
    }
    
    private void OnPuzzlePiecePlaced(int stageIndex, int difficulty, List<PuzzlePiecePosition> pieces)
    {
        
    }

    private void OnClearPuzzle(int stageIndex, int difficulty, float clearTime)
    {
        Debug.Log("OnClearPuzzle()->");
        
        clearPopup.SetActive(true);
    }

    void Start()
    {
        // 버튼 클릭 시 ChangeSprites 메서드 호출
        if (changeSpriteButton == null)
        {
            Debug.LogError("Change Sprite Button is not assigned!");
        }
        else
        {
            changeSpriteButton.onClick.AddListener(ChangeSprites);  
            
            Debug.Log("Change Sprite Button is assigned!");
        }
    }

    void ChangeSprites()
    {
        Debug.Log("ChangeSprites()->");
        // // PuzzleManager의 ChangePuzzlePieceSprites 메서드를 호출하여 스프라이트를 변경
        // Sprite newSprite = SpriteManager.Instance.GetRandomSprite();
        //
        // puzzleManager.ChangePuzzlePieceSprites(newSprite);
    }

    public void OnBackButton()
    {
        Debug.Log("OnBackButton()->");

        PuzzleManager.Instance.EndGame();
        
        SceneLoader.Instance.LoadMainMenuScene();
    }

    public void OnClearButton()
    {
        Debug.Log("OnClearButton()->");
        
        PuzzleManager.Instance.EndGame();
        
        SceneLoader.Instance.LoadMainMenuScene();
    }

}
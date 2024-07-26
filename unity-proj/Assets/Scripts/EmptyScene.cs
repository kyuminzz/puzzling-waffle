using System.Collections.Generic;
using UnityEngine;

public class EmptyScene : MonoBehaviour
{
    private StageProgressData _stageProgressData;

    void Start()
    {
        _stageProgressData = StageProgressStorage.Instance.LoadData();
    }
    
    public void Test_Stage_2_2()
    {
        SaveProgress(2, 2, new List<PuzzlePiecePosition>
        {
            new PuzzlePiecePosition(1, 2),
            new PuzzlePiecePosition(3, 4),
        });
    }
    public void Test_Stage_2_1()
    {
        SaveProgress(2, 2, new List<PuzzlePiecePosition>
        {
            new PuzzlePiecePosition(1, 2)
        });
    }
    
    public void Test_StageClear()
    {
        MarkStageAsCleared(1, 4, 123.45f);
    }
    
    public void Test_Show()
    {
        foreach (var progress in _stageProgressData.InProgressStages)
        {
            Debug.Log($"StageIndex: {progress.StageIndex}, Difficulty: {progress.Difficulty}");
            foreach (var piece in progress.Pieces)
            {
                Debug.Log($"Piece: ({piece.X}, {piece.Y})");
            }
        }

        foreach (var clearedStage in _stageProgressData.ClearedStages)
        {
            Debug.Log($"StageIndex: {clearedStage.StageIndex}, Difficulty: {clearedStage.Difficulty}, ClearTime: {clearedStage.ClearTime}");
        }
    }

    public void Test_Remove()
    {
        StageProgressStorage.Instance.ClearData();
        _stageProgressData = StageProgressStorage.Instance.LoadData();
    }

    void SaveProgress(int stageIndex, int difficulty, List<PuzzlePiecePosition> pieces)
    {
        PuzzleProgressInfo progressInfo = new PuzzleProgressInfo(stageIndex, difficulty);
        progressInfo.Pieces.AddRange(pieces);

        if (_stageProgressData.InProgressStages.Exists(p => (p.StageIndex == stageIndex && p.Difficulty == difficulty)))
        {
            _stageProgressData.InProgressStages.RemoveAll(p =>
                (p.StageIndex == stageIndex && p.Difficulty == difficulty));
        }

        _stageProgressData.InProgressStages.Add(progressInfo);

        StageProgressStorage.Instance.SaveData(_stageProgressData);
    }

    void MarkStageAsCleared(int stageIndex, int difficulty, float clearTime)
    {
        PuzzleClearInfo clearInfo = new PuzzleClearInfo(stageIndex, difficulty, clearTime);
        _stageProgressData.ClearedStages.Add(clearInfo);
        StageProgressStorage.Instance.SaveData(_stageProgressData);
    }
}
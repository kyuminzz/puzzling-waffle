using System.Collections.Generic;

public class PuzzleProgressInfo
{
    public int StageIndex { get; set; }
    public int Difficulty { get; set; } // 난이도 (예: 4, 36 등)
    public List<PuzzlePiecePosition> Pieces { get; set; } // 퍼즐 조각들의 좌표 정보

    public PuzzleProgressInfo(int stageIndex, int difficulty)
    {
        StageIndex = stageIndex;
        Difficulty = difficulty;
        Pieces = new List<PuzzlePiecePosition>();
    }
}

public class PuzzlePiecePosition
{
    public int X { get; set; }
    public int Y { get; set; }

    public PuzzlePiecePosition(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class PuzzleClearInfo
{
    public int StageIndex { get; set; }
    public int Difficulty { get; set; }
    public float ClearTime { get; set; } // 클리어 시간 (초 단위)

    public PuzzleClearInfo(int stageIndex, int difficulty, float clearTime)
    {
        StageIndex = stageIndex;
        Difficulty = difficulty;
        ClearTime = clearTime;
    }
}

public class StageProgressData
{
    public List<PuzzleProgressInfo> InProgressStages { get; set; }
    public List<PuzzleClearInfo> ClearedStages { get; set; }

    public StageProgressData()
    {
        InProgressStages = new List<PuzzleProgressInfo>();
        ClearedStages = new List<PuzzleClearInfo>();
    }

    
}
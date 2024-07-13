using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EControlType
{
    Keyboard,
    Mouse,
    Gamepad
}

public enum EPuzzleDifficulty
{
    Beginner4 = 16,
    Beginner5 = 25,
    Easy6 = 36,
    Easy7 = 49,
    Easy8 = 64,
    Medium9 = 81,
    Medium10 = 100,
    Hard11 = 121,
    Hard12 = 144,
    Expert13 = 169,
    Expert14 = 196,
    Master15 = 225,
    Master16 = 256,
    Legend17 = 289,
    Legend18 = 324,
    Mythic19 = 361,
    Mythic20 = 400
}

public class PlayerSettings : MonoBehaviour
{
    public static EPuzzleDifficulty puzzleDifficulty = EPuzzleDifficulty.Beginner4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

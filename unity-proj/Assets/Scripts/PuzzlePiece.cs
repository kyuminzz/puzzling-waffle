using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] public PuzzlePiecePosition GridPosition;
    public static Action OnRightPosition;
    public bool isDebug = false;
    private const float LEFT_BOUNDARY = -1.25f;
    private const float RIGHT_BOUNDARY = 1.25f;
    private const float UP_BOUNDARY = 0.3f;
    private const float DOWN_BOUNDARY = -4.3f;
    
    private Vector3 RightPosition = Vector3.one;
    public bool Selected { get; set; }
    public bool InRightPosition { get; set; }

    void Awake()
    {
        if(RightPosition == Vector3.one)
            RightPosition = transform.localPosition;
    }
    void Start()
    {
        //MoveToRandomPosition();
    }
    private void Log(string message)
    {
        if (isDebug)
            Debug.Log(message);
    }
    public void MoveToRandomPosition()
    {
        Log($"MoveToRandomPosition()->RightPosition:{RightPosition}");

        transform.localPosition = new Vector3(Random.Range(LEFT_BOUNDARY, RIGHT_BOUNDARY), Random.Range(UP_BOUNDARY, DOWN_BOUNDARY), 0f);

        InRightPosition = false;
    }
    public void MoveToRightPosition()
    {
        transform.localPosition = RightPosition;
        InRightPosition = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Log($"MoveToRandomPosition()->localPosition:{transform.localPosition}, RightPosition:{RightPosition}, Distance:{Vector3.Distance(transform.localPosition, RightPosition)}");
        }
        
        if(InRightPosition == false && Vector3.Distance(transform.localPosition, RightPosition) < 0.5f)
        {
            if (!Selected)
            {
                MoveToRightPosition();
                OnRightPosition?.Invoke();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private const float LEFT_BOUNDARY = -1.25f;
    private const float RIGHT_BOUNDARY = 1.25f;
    private const float UP_BOUNDARY = 0.3f;
    private const float DOWN_BOUNDARY = -4.3f;
    
    private Vector3 RightPosition;
    public bool Selected { get; set; }
    public bool InRightPosition { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(LEFT_BOUNDARY, RIGHT_BOUNDARY),
            Random.Range(UP_BOUNDARY, DOWN_BOUNDARY), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, RightPosition) < 0.5f)
        {
            if (!Selected)
            {
                transform.position = RightPosition;
                InRightPosition = true;
            }
        }
    }
}

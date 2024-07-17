using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class DragAndDrop : MonoBehaviour
{
    public GameObject SelectedObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition),
                Vector2.zero);
            if (hit.transform != null && hit.transform.CompareTag("PuzzlePiece"))
            {
                if (!hit.transform.GetComponent<PuzzlePiece>().InRightPosition)
                {
                    SelectedObject = hit.transform.gameObject;
                    SelectedObject.GetComponent<PuzzlePiece>().Selected = true;
                }
            }
        }

        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            if (SelectedObject == null)
                return;
            var puzzlePiece = SelectedObject.GetComponent<PuzzlePiece>();
            if (puzzlePiece == null)
                return;
            SelectedObject.GetComponent<PuzzlePiece>().Selected = false;
            SelectedObject = null;

        }

        if (SelectedObject != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            SelectedObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        }
    }
}

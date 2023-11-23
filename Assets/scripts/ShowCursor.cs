using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCursor : MonoBehaviour
{
    [SerializeField]
    private Texture2D ReleasedState;

    [SerializeField]
    private Texture2D PressedState;

    private Vector2 _hotspot = Vector2.zero;

    [SerializeField]
    private CursorMode _cursorMode = CursorMode.Auto;

    //cursor initial
    void Start()
    {
        Cursor.SetCursor(ReleasedState, _hotspot, _cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        //different states of cursor
        if(Input.GetMouseButton(0))
        {
            Cursor.SetCursor(PressedState, _hotspot, _cursorMode);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(ReleasedState, _hotspot, _cursorMode);
        }

    }
}

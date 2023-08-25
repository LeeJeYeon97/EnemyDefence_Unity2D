using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private Vector2 moveDir;
    public Vector2 MoveDir
    {
        get { return moveDir; }
        set 
        { 
            moveDir = value;
        }
    }
    private void OnMove(InputValue value)
    {
        MoveDir = value.Get<Vector2>();
    }
    
}

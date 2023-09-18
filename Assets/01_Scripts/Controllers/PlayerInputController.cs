
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.EnhancedTouch;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

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
    private void OnEnable()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();

        EnhancedTouch.Touch.onFingerDown += FingerDown;
        EnhancedTouch.Touch.onFingerUp += FingerUp;
    }
    private void OnDisable()
    {
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();

        EnhancedTouch.Touch.onFingerDown -= FingerDown;
        EnhancedTouch.Touch.onFingerUp -= FingerUp;
    }
    
    private void FingerDown(Finger finger)
    {
        Vector2 pos = finger.screenPosition;
        
        if(pos.x == Mathf.Infinity || pos.y == Mathf.Infinity)
        {
            Debug.Log("test");
        }
        else
        {
            GameManager.Instance.joyStick.transform.position = pos;
            GameManager.Instance.joyStick.gameObject.SetActive(true);
        }
    }
    private void FingerUp(Finger finger)
    {
        GameManager.Instance.joyStick.gameObject.SetActive(false);
    }
    
}


using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerInputController : MonoBehaviour
{
    public delegate void StartTouchEvent(Vector2 position, float time);
    public static event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public static event EndTouchEvent OnEndTouch;

    private Vector2 moveDir;
    public Vector2 MoveDir
    {
        get { return moveDir; }
        set 
        { 
            moveDir = value;
        }
    }
    // 터치 스크린 사용
    private Test touchControls;

    private void Awake()
    {
        touchControls = new Test();
    }
    private void OnEnable()
    {
        touchControls.Enable();
        TouchSimulation.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += FingerUp;
    }
    private void OnDisable()
    {
        touchControls.Disable();
        TouchSimulation.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= FingerUp;
    }
    private void OnMove(InputValue value)
    {
        MoveDir = value.Get<Vector2>();
    }

    //private void Start()
    //{
    //    //touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
    //    //touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    //
    //}
    //
    //private void StartTouch(InputAction.CallbackContext context)
    //{
    //    Debug.Log("Touch started" + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
    //    if(OnStartTouch != null)
    //    {
    //        OnStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(),
    //            (float)context.startTime);
    //        
    //    }
    //    
    //}
    //private void EndTouch(InputAction.CallbackContext context)
    //{
    //    Debug.Log("Touch Ended");
    //    if (OnEndTouch != null)
    //    {
    //        OnEndTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(),
    //            (float)context.time);
    //    }
    //    
    //}

    private void FingerDown(Finger finger)
    {
        Vector2 pos = finger.screenPosition;
        Debug.Log(pos);
        if(pos.x == Mathf.Infinity || pos.y == Mathf.Infinity)
        {

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

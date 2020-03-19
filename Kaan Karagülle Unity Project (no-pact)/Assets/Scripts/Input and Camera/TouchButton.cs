using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    enum ButtonType
    {
        Left,
        Right,
        Up,
    }

    [SerializeField] ButtonType buttonType; //button type is set from the inspector
    bool buttonPressed = false;

    private void Awake()
    {
        //disables the touch control button if the platform is mobile
        //They're also left visible in the editor for testing
        #if !UNITY_ANDROID && !UNITY_IOS && !UNITY_EDITOR
        gameObject.SetActive(false);
        #endif
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        if (buttonType == ButtonType.Up) { InputManager.Instance.Jump(); }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    void Update()
    {
        if (!buttonPressed) { return; }
        if (InputManager.Instance != null)
        {
            if (buttonType == ButtonType.Left) { InputManager.Instance.LeftTouch(); }
            else if (buttonType == ButtonType.Right) { InputManager.Instance.RightTouch(); } 
        }
    }
}

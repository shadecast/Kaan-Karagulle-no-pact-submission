using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    Controls controls;
    PlayableCharacter player;

    float horizontalMovement;

    float deadzoneValue = 0.3f;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); }
        else { Instance = this; }
        player = FindObjectOfType<PlayableCharacter>();
        SetupControls();
    }

    //the update function doesn't run on mobile platforms since a different logic is used for them
    #if !UNITY_ANDROID && !UNITY_IOS
    void Update()
    {
        if (Mathf.Abs(horizontalMovement) > deadzoneValue) {  player.MoveHorizontal(horizontalMovement); }
    }
    #endif

    public void LeftTouch()
    {
        player.MoveHorizontal(-1);
    }

    public void RightTouch()
    {
        player.MoveHorizontal(1);
    }

    public void Jump()
    {
        player.Jump();
    }

    private void SetupControls()
    {
        controls = new Controls();
        controls.Gameplay.Move.performed += ctx => horizontalMovement = ctx.ReadValue<float>();
        controls.Gameplay.Move.canceled += ctx => horizontalMovement = 0f;
        controls.Gameplay.Jump.started += ctx => Jump();
        controls.Gameplay.Enable();
    }
}

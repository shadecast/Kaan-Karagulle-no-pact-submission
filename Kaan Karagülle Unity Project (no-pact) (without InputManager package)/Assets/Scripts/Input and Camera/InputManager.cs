using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    PlayableCharacter player;

    float horizontalMovement;

    float deadzoneValue = 0.3f;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); }
        else { Instance = this; }
        player = FindObjectOfType<PlayableCharacter>();
    }

    //the update function doesn't run on mobile platforms since a different logic is used for them
    #if !UNITY_ANDROID && !UNITY_IOS
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontalMovement) > deadzoneValue) { player.MoveHorizontal(horizontalMovement); }
        if (Input.GetButtonDown("Jump")) { Jump(); }
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
}

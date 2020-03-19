using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacter : MonoBehaviour
{
    //player attributes, can be adjusted from the inspector
    [SerializeField] float movementSpeed = 7f;
    [SerializeField] float jumpSpeed = 18f;
    [SerializeField] protected float characterWeight = 5f;
    
    //references
    [SerializeField] protected Collider2D feetHitbox; //A trigger collider for determining if the player is grounded. Referanced through the inspector
    protected Rigidbody2D playerRigidbody;
    SpriteRenderer spriteRenderer;

    //abstract methods, since their functionality vary depending on the character
    public abstract void Attack();
    public abstract void UseJumpSpecialty();
    public abstract void CancelJumpSpecialty();

    //bool states
    protected bool canMove = true;
    protected bool canJump = true;
    protected bool jumpSpecialtyAvailable = false;
    protected bool usingJumpSpecialty = false;
    protected bool isFacingLeft = true;

    //grabs the necessary components from the game object and sets it's rigidbody gravityScale to the characterWeight
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        if (playerRigidbody != null) { playerRigidbody.gravityScale = characterWeight; }
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    //moves the character in the horizontal axis, takes an input range of [-1, 1]
    public void MoveHorizontal(float direction)
    {
        if (!canMove) { return; }

        direction = Mathf.Clamp(direction, -1f, 1f);    
        playerRigidbody.velocity = new Vector2(direction * movementSpeed, playerRigidbody.velocity.y);

        if (direction < 0) { isFacingLeft = true; }
        else { isFacingLeft = false; }
        UpdatePlayerOrientation();
    }

    //feet collider need to be touching a ground layer to jump off the ground, otherwise uses the jump specialty if it's available
    public void Jump()
    {
        if (!canJump) { return; }

        if (feetHitbox != null && feetHitbox.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed);
            jumpSpecialtyAvailable = true;
        }
        else if (jumpSpecialtyAvailable) { UseJumpSpecialty(); }
    }

    //flips the player sprite to face the appropriate direction
    private void UpdatePlayerOrientation()
    {
        if (spriteRenderer != null)
        {
            if (isFacingLeft) { spriteRenderer.transform.localScale = new Vector2(1, spriteRenderer.transform.localScale.y); }
            else { spriteRenderer.transform.localScale = new Vector2(-1, spriteRenderer.transform.localScale.y); }
        }
    }

    //collision cancels jump specialty if it's currently being used
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (usingJumpSpecialty)
        {
            CancelJumpSpecialty();
            jumpSpecialtyAvailable = false;
        }
    }
}
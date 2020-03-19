using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alice : PlayableCharacter
{
    //dash properties, can be adjusted from the inspector
    [SerializeField] bool dashCarriesMomentum = false;
    [SerializeField] float dashVelocity = 27f;
    [SerializeField] float dashTime = 0.2f;

    Coroutine dash;

    public override void Attack()
    {
        //attack logic for Alice
    }

    //initiates the dash coroutine
    public override void UseJumpSpecialty()
    {
        if (dash != null) { StopCoroutine(dash); }
        dash = StartCoroutine(Dash());
    }

    //handles the dash logic. doesn't allow movement and jumping mid-dash. player isn't affected by gravity while dashing
    IEnumerator Dash()
    {
        usingJumpSpecialty = true;
        jumpSpecialtyAvailable = false;
        canMove = false;
        canJump = false;

        playerRigidbody.gravityScale = 0;
        if (isFacingLeft) { playerRigidbody.velocity = new Vector2(-dashVelocity, 0f); }
        else { playerRigidbody.velocity = new Vector2(dashVelocity, 0f); }

        float dashTimer = 0f;
        while (dashTimer < dashTime)
        {
            yield return null;
            dashTimer += Time.deltaTime;
        }

        if (!dashCarriesMomentum) { playerRigidbody.velocity = Vector2.zero; }
        playerRigidbody.gravityScale = characterWeight;
        canMove = true;
        canJump = true;
        usingJumpSpecialty = false;
    }

    //stops the dash coroutine and turn player movement back on
    public override void CancelJumpSpecialty()
    {
        if (dash != null) { StopCoroutine(dash); }
        if (!dashCarriesMomentum) { playerRigidbody.velocity = Vector2.zero; }
        playerRigidbody.gravityScale = characterWeight;
        canMove = true;
        canJump = true;
        usingJumpSpecialty = false;
    }
}
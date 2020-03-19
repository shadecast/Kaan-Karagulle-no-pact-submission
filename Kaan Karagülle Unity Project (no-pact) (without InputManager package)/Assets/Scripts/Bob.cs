using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : PlayableCharacter
{
    //ground slam properties, can be adjusted from the inspector
    [SerializeField] float groundSlamVelocity = 35f;
    [SerializeField] float waitTimeBeforeSlam = 0.2f;

    Coroutine groundSlam;
   
    public override void Attack()
    {
        //attack logic for Bob
    }

    //initiates the groundSlam coroutine
    public override void UseJumpSpecialty()
    {
        if (groundSlam != null) { StopCoroutine(groundSlam); }
        groundSlam = StartCoroutine(GroundSlam());
    }

    //handles the ground slam logic. doesn't allow movement and jumping mid-dash. stops when bob hit the ground of when ground slam is cancelled
    IEnumerator GroundSlam()
    {
        usingJumpSpecialty = true;
        jumpSpecialtyAvailable = false;
        canMove = false;
        canJump = false;

        if (waitTimeBeforeSlam > 0f) //hovers for a set amount of seconds then slams the ground
        {
            playerRigidbody.gravityScale = 0f;
            playerRigidbody.velocity = Vector2.zero;
            float waitTime = 0f;
            while (waitTime < waitTimeBeforeSlam)
            {
                yield return null;
                waitTime += Time.deltaTime;
            }
            playerRigidbody.gravityScale = characterWeight;
        }

        int groundLayerMask = LayerMask.GetMask("Ground");
        while (feetHitbox != null && !feetHitbox.IsTouchingLayers(groundLayerMask))
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, -groundSlamVelocity);
            yield return null;
        }        

        canMove = true;
        canJump = true;
        usingJumpSpecialty = false;
    }

    //stops the groundSlam coroutine and turn player movement back on
    public override void CancelJumpSpecialty()
    {
        if (groundSlam != null) { StopCoroutine(groundSlam); }
        playerRigidbody.gravityScale = characterWeight;
        canMove = true;
        canJump = true;
        usingJumpSpecialty = false;
    }
}

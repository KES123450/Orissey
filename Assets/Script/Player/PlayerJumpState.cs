using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerJumpState : MonoBehaviour, IPlayerState
{
    private PlayerController playerController;
    private bool isJumping;
    public void Handle(PlayerController controller)
    {
        if (isJumping) return;
        controller.DuckAnimator.SetTrigger("isJump");
        controller.DuckAnimator.ResetTrigger("isGrounded");
        controller.playerAnim.Play(PlayerAnimationType.JumpAni);
        StartCoroutine(DelayedJump(controller));
        
    }

    private IEnumerator DelayedJump(PlayerController controller)
    {
        isJumping = true;
        yield return new WaitForSeconds(0.25f);
        controller.playerRigid.AddForce(controller.jumpDirection * controller.jumpForce, ForceMode2D.Impulse);
        controller.isJump = false;
        isJumping = false;
    }
}

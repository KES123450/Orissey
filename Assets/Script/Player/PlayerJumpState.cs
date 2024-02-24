using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : MonoBehaviour,IPlayerState
{
    public void Handle(PlayerController controller)
    {
        controller.playerRigid.AddForce(Vector2.up * controller.jumpForce,ForceMode2D.Impulse);
        controller.isJump = false;

    }
}

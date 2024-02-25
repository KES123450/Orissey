using UnityEngine;

public class PlayerJumpState : MonoBehaviour, IPlayerState
{
    private PlayerController playerController;

    public void Handle(PlayerController controller)
    {
        controller.playerRigid.AddForce(Vector2.up*controller.jumpForce, ForceMode2D.Impulse);
         
    }
}

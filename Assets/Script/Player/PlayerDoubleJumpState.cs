using UnityEngine;

public class PlayerDoubleJumpState : MonoBehaviour, IPlayerState
{
    private PlayerController playerController;

    public void Handle(PlayerController controller)
    {
        controller.time += Time.deltaTime;
        if (controller.time < controller.GoalTime)
        {
            controller.rigid.velocity = (Vector2)(transform.right * controller.walkForce + controller.gravity) + new Vector2(0, 0.1f * controller.jumpAnimation.Evaluate(controller.time) * controller.jumpForce);
            Debug.Log(controller.jumpAnimation.Evaluate(controller.time));

        }
        else
        {
            controller.time = 0;
            controller.isDoubleJump = false;
        }
    }

}
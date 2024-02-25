using UnityEngine;

public class PlayerMoveState : MonoBehaviour,IPlayerState
{
    private PlayerController playerController;

    public void Handle(PlayerController controller)
    {
        RigidbodyConstraints2D constraints = RigidbodyConstraints2D.None;
        controller.backWheelRigid.constraints = constraints;
        controller.frontWheelRigid.constraints = constraints;
        controller.backWheel.useMotor = true;
    }
}

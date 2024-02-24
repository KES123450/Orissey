using UnityEngine;

public class PlayerMoveState : MonoBehaviour,IPlayerState
{

    public void Handle(PlayerController controller)
    {
        controller.backWheel.useMotor = true;
        RigidbodyConstraints2D rigidConstraints = RigidbodyConstraints2D.None;
        controller.backWheelRigid.constraints = rigidConstraints;
        controller.frontWheelRigid.constraints = rigidConstraints;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : MonoBehaviour, IPlayerState
{
    public void Handle(PlayerController controller)
    {
        controller.backWheel.useMotor = false;
        RigidbodyConstraints2D rigidConstraints = RigidbodyConstraints2D.FreezeRotation;
        controller.backWheelRigid.constraints = rigidConstraints;
        controller.frontWheelRigid.constraints = rigidConstraints;

    }
}

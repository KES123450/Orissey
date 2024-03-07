using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : MonoBehaviour, IPlayerState
{
    public void Handle(PlayerController controller)
    {
        
        controller.backWheel.useMotor = false;
        RigidbodyConstraints2D constraints = RigidbodyConstraints2D.FreezeRotation;
        controller.backWheelRigid.constraints = constraints;
        controller.frontWheelRigid.constraints = constraints;
    }
}

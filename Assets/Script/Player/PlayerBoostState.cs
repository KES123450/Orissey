using UnityEngine;

public class PlayerBoostState : MonoBehaviour, IPlayerState
{
    private PlayerController playerController;

    public void Handle(PlayerController controller)
    {
        //controller.rigid.velocity = controller.velocity * controller.maxBoostForce;
    }

}

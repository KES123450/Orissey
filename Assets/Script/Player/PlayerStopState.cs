using UnityEngine;

public class PlayerStopState : MonoBehaviour, IPlayerState
{
    public void Handle(PlayerController controller)
    {
        controller.rigid.velocity = new Vector2(0, 0);
    }
}

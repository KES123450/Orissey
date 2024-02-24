using UnityEngine;

public class PlayerMoveState : MonoBehaviour,IPlayerState
{
    private PlayerController playerController;

    public void Handle(PlayerController controller)
    {
        /*if(controller.walkForce>= controller.maxWalkForce)
        {
            controller.walkForce = controller.maxWalkForce;
        }
        else
        {
            controller.walkForce += 0.1f;
        }
        controller.rigid.velocity = controller.velocity * controller.walkForce + controller.gravity;*/

        if(controller.rigid.velocity.magnitude >= controller.maxWalkForce)
        {
            controller.rigid.velocity = controller.rigid.velocity.normalized * controller.maxWalkForce;
        }
        else
        {
            controller.rigid.AddForce(controller.transform.right * controller.walkForce);
        }
        

    }
}

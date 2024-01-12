using UnityEngine;

public class PlayerRotateState : MonoBehaviour, IPlayerState
{
    private PlayerController playerController;

    public void Handle(PlayerController controller)
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            controller.transform.localEulerAngles += new Vector3(0, 0, 0.01f * controller.anglePos);
            controller.boostCheck += 0.01f * controller.anglePos;

            if (controller.boostCheck >= 360)
            {
                controller.isBoost = true;
                controller.boostCheck = 0;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            controller.transform.localEulerAngles -= new Vector3(0, 0, 0.01f * controller.anglePos);
            controller.boostCheck -= 0.01f * controller.anglePos;
        }
    }
}

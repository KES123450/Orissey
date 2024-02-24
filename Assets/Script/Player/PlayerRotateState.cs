using UnityEngine;

public class PlayerRotateState : MonoBehaviour, IPlayerState
{
    private PlayerController playerController;
    [SerializeField] private float rotateForce;
    public void Handle(PlayerController controller)
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            controller.transform.localEulerAngles += new Vector3(0, 0, rotateForce * controller.anglePos);
            controller.SetBoostCheck(rotateForce * controller.anglePos);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            controller.transform.localEulerAngles += new Vector3(0, 0, -rotateForce * controller.anglePos);
            controller.SetBoostCheck(-rotateForce * controller.anglePos);
        }
    }
}

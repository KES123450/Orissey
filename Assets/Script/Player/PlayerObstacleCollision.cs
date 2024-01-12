using UnityEngine;

public class PlayerObstacleCollision : MonoBehaviour
{
    public PlayerController playerController;

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.collider.CompareTag("Obstacle"))
        {
            Debug.Log("��������");
            playerController.SetStop();
        }
    }
}

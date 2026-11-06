
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private ShieldPowerUp shieldPowerUp;
    private PlayerController playerController;

    void Start()
    {
        shieldPowerUp = GetComponent<ShieldPowerUp>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Use the shield if available
            if (shieldPowerUp != null && shieldPowerUp.UseShield())
            {
                Debug.Log("Shield saved the player!");
                Destroy(other.gameObject);
                return;
            }

            // No shield: Game Over
            GameOverManager gameOver =
                FindFirstObjectByType<GameOverManager>();

            if (gameOver != null)
                gameOver.GameOver();

            if (playerController != null)
                playerController.enabled = false;
        }
    }
}

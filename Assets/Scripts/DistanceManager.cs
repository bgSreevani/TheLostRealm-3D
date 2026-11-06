
using UnityEngine;
using TMPro;

public class DistanceManager : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;
    public PlayerController playerController;

    [Header("UI Settings")]
    public TextMeshProUGUI distanceText;

    [Header("Speed Settings")]
    public float speedIncreaseRate = 0.2f;
    public float maximumSpeed = 18f;

    private float startingPosition;
    private int distance;

    void Start()
    {
        startingPosition = player.position.z;
    }

    void Update()
    {
        if (player == null)
            return;

        // Calculate distance travelled
        distance = Mathf.Max(
            0,
            Mathf.FloorToInt(player.position.z - startingPosition)
        );

        // Update distance display
        if (distanceText != null)
        {
            distanceText.text = "Distance: " + distance + " m";
        }

        // Gradually increase running speed
        if (playerController != null &&
            playerController.enabled)
        {
            playerController.forwardSpeed = Mathf.Min(
                playerController.forwardSpeed +
                speedIncreaseRate * Time.deltaTime,
                maximumSpeed
            );
        }
    }
    public int GetDistance()
{
    return distance;
}
}

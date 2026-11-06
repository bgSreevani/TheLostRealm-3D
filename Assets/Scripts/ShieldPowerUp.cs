using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public bool hasShield = false;

    public GameObject shieldVisual;

    private void Start()
    {
        if (shieldVisual != null)
            shieldVisual.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasShield = true;

            if (shieldVisual != null)
                shieldVisual.SetActive(true);

            Debug.Log("MAGIC SHIELD ACTIVATED!");

            Destroy(other.gameObject);
        }
    }

    public bool UseShield()
    {
        if (hasShield)
        {
            hasShield = false;

            if (shieldVisual != null)
                shieldVisual.SetActive(false);

            Debug.Log("Shield protected the player!");
            return true;
        }

        return false;
    }
}
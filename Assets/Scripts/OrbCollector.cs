using UnityEngine;
using TMPro;

public class OrbCollector : MonoBehaviour
{
    public int orbCount = 0;
    public TextMeshProUGUI orbCountText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip orbCollectSound;

    [Header("Visual Effects")]
    public ParticleSystem orbCollectEffect;

    void Start()
    {
        UpdateScore();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            orbCount++;
            UpdateScore();

            if (audioSource != null && orbCollectSound != null)
            {
                audioSource.PlayOneShot(orbCollectSound);
            }

            if (orbCollectEffect != null)
            {
                ParticleSystem effect = Instantiate(
                    orbCollectEffect,
                    other.transform.position,
                    Quaternion.identity
                );

                effect.Play();

                Destroy(
                    effect.gameObject,
                    effect.main.duration +
                    effect.main.startLifetime.constantMax
                );
            }

            Debug.Log("Orbs Collected: " + orbCount);
            Destroy(other.gameObject);
        }
    }

    void UpdateScore()
    {
        if (orbCountText != null)
        {
            orbCountText.text = "Orbs: " + orbCount;
        }
    }
}
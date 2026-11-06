using UnityEngine;
using System.Collections;

public class SpeedBoostController : MonoBehaviour
{
    public float speedMultiplier = 1.8f;
    public float boostDuration = 5f;

    private PlayerController playerController;
    private Coroutine boostRoutine;
    private CameraFollow cameraFollow;

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        if (Camera.main != null)
            cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedBoost"))
        {
            Destroy(other.gameObject);

            if (playerController != null)
            {
                if (boostRoutine != null)
                    StopCoroutine(boostRoutine);

                boostRoutine = StartCoroutine(ActivateBoost());
            }

            Debug.Log("SPEED BOOST ACTIVATED!");
        }
    }

    IEnumerator ActivateBoost()
    {
        float originalSpeed = playerController.forwardSpeed;

        playerController.forwardSpeed *= speedMultiplier;

        if (cameraFollow != null)
        {
            Debug.Log("Camera FOV Boost Started!");
            cameraFollow.SpeedBoostEffect(true);
        }
        else
        {
            Debug.LogError("CameraFollow reference is NULL!");
        }

        yield return new WaitForSeconds(boostDuration);

        playerController.forwardSpeed = originalSpeed;

        if (cameraFollow != null)
        {
            cameraFollow.SpeedBoostEffect(false);
            Debug.Log("Camera FOV Boost Ended!");
        }

        boostRoutine = null;
    }
}
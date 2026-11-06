
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public Vector3 offset = new Vector3(0f, 5f, -8f);

    public float smoothSpeed = 5f;

    public float bobAmount = 0.08f;
    public float bobSpeed = 8f;

    private float startFOV;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (cam != null)
            startFOV = cam.fieldOfView;
    }

    void LateUpdate()
    {
        if (player == null) return;

        float bob = Mathf.Sin(Time.time * bobSpeed) * bobAmount;

        Vector3 targetPosition =
            player.position + offset + Vector3.up * bob;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(player.position + Vector3.up * 1.5f);
    }

    public void SpeedBoostEffect(bool active)
    {
        if (cam == null) return;

        cam.fieldOfView = active
            ? startFOV + 10f
            : startFOV;
    }
}

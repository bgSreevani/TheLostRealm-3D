using UnityEngine;

public class FlyingBookAnimation : MonoBehaviour
{
    public float rotationSpeed = 80f;
    public float floatHeight = 0.3f;
    public float floatSpeed = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(
            Vector3.up * rotationSpeed * Time.deltaTime
        );

        float newY = startPosition.y +
            Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );
    }
}
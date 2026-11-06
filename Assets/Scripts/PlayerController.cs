using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Running")]
    public float forwardSpeed = 8f;

    [Header("Lane Movement")]
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;

    [Header("Jump Settings")]
    public float jumpForce = 7f;
    public ParticleSystem jumpEffect;

    [Header("Slide Settings")]
    public float slideDuration = 1f;
    public float slideHeight = 1f;
    public ParticleSystem slideEffect;

    private int currentLane = 1;
    private Rigidbody rb;
    private CapsuleCollider capsule;

    private bool isGrounded = true;
    private bool isSliding = false;

    private float originalHeight;
    private Vector3 originalCenter;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();

        originalHeight = capsule.height;
        originalCenter = capsule.center;
    }

    void Update()
    {
        // Lane movement
        if (Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.A))
        {
            currentLane = Mathf.Max(0, currentLane - 1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.D))
        {
            currentLane = Mathf.Min(2, currentLane + 1);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Slide
        if (Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.S))
        {
            if (!isSliding)
                StartCoroutine(Slide());
        }
    }

    void FixedUpdate()
    {
        float targetX = (currentLane - 1) * laneDistance;

        float newX = Mathf.MoveTowards(
            rb.position.x,
            targetX,
            laneChangeSpeed * Time.fixedDeltaTime
        );

        Vector3 movement = new Vector3(
            newX,
            rb.position.y,
            rb.position.z + forwardSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(movement);
    }

    void Jump()
    {
        if (isGrounded && !isSliding)
        {
            isGrounded = false;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (jumpEffect != null)
            {
                jumpEffect.Play();
            }
        }
    }

    IEnumerator Slide()
    {
        isSliding = true;

        capsule.height = slideHeight;
        capsule.center = new Vector3(
            originalCenter.x,
            slideHeight / 2f,
            originalCenter.z
        );

        if (slideEffect != null)
        {
            slideEffect.Play();
        }

        yield return new WaitForSeconds(slideDuration);

        if (slideEffect != null)
        {
            slideEffect.Stop();
        }

        capsule.height = originalHeight;
        capsule.center = originalCenter;

        isSliding = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
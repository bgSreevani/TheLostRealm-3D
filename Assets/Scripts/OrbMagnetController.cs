
using UnityEngine;
using System.Collections;

public class OrbMagnetController : MonoBehaviour
{
    public float attractionRadius = 12f;
    public float attractionSpeed = 12f;
    public float magnetDuration = 10f;

    private bool magnetActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OrbMagnet"))
        {
            Destroy(other.gameObject);

            StopAllCoroutines();
            StartCoroutine(ActivateMagnet());

            Debug.Log("ORB MAGNET ACTIVATED!");
        }
    }

    IEnumerator ActivateMagnet()
    {
        magnetActive = true;

        yield return new WaitForSeconds(magnetDuration);

        magnetActive = false;

        Debug.Log("ORB MAGNET ENDED!");
    }

    void Update()
    {
        if (!magnetActive) return;

        Collider[] nearbyOrbs = Physics.OverlapSphere(
            transform.position,
            attractionRadius
        );

        foreach (Collider orb in nearbyOrbs)
        {
            if (orb.CompareTag("Collectible"))
            {
                orb.transform.position = Vector3.MoveTowards(
                    orb.transform.position,
                    transform.position,
                    attractionSpeed * Time.deltaTime
                );
            }
        }
    }
}

using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public PlayerController playerController;

    IEnumerator Start()
    {
        playerController.enabled = false;

        string[] countdown = { "3", "2", "1", "GO!" };

        foreach (string value in countdown)
        {
            countdownText.text = value;
            yield return new WaitForSecondsRealtime(1f);
        }

        countdownText.gameObject.SetActive(false);
        playerController.enabled = true;
    }
}
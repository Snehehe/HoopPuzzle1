using UnityEngine;

public class BallBounceSound : MonoBehaviour
{
    private AudioSource bounceSound;
    private float playStartTime = 6.9f;
    private float playDuration = 0.2f; // 7.1 - 6.9 = 0.2 seconds

    void Start()
    {
        bounceSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 0.2f) // Adjust for sensitivity
        {
            bounceSound.time = playStartTime; // Start playback at 6.9 seconds
            bounceSound.Play();
            StartCoroutine(StopAudioAfterDuration());
        }
    }

    private System.Collections.IEnumerator StopAudioAfterDuration()
    {
        yield return new WaitForSeconds(playDuration);
        bounceSound.Stop();
    }
}

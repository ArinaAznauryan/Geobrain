using UnityEngine;

public class Comet : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayComet()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned!");
        }
    }
}

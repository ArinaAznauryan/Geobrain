using UnityEngine;
using UnityEngine.UI;

public class ToggleMusic : MonoBehaviour
{
    private AudioSource backgroundMusic;
    private bool isMusicPlaying = true;

    void Start()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            if (source.gameObject.name == "BgSound")
            {
                backgroundMusic = source;
                break;
            }
        }

    }

    public void ToggleMusicState()
    {
        if (isMusicPlaying)
        {
            backgroundMusic.Pause();
        }
        else
        {
            backgroundMusic.Play();
        }
        isMusicPlaying = !isMusicPlaying;
    }
}

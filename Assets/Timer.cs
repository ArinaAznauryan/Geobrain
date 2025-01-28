using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component
    public float timerDuration = 300f; // 5 minutes in seconds
    private float timer; // Current time left
    //public Animator animator; // Reference to the Animator component

    void Start()
    {
        timer = timerDuration; // Initialize the timer with the duration
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // Decrease the timer by the time passed since last frame

            // Calculate minutes and seconds
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);

            // Update the TextMeshPro text
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = "00:00";
            Debug.Log("time is up");
            //animator.Play("timerAnimation"); // Play the animation
            
            enabled = false; // Disable this script to stop further updates
        }
    }
    
}

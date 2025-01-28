using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ResourcesQuestions : MonoBehaviour
{
    public GetEarthResources resourcesrandom;
    private Coroutine fadeCoroutine;
    public TMP_InputField answerInput;
    public TextMeshProUGUI wrongText;
    public UnityEvent wrongsound;
    public UnityEvent correctsound;

    private Vector3 inputFieldOriginalPosition;

    private void Start()
    {
        // Add event listener to detect Enter key press
        answerInput.onSubmit.AddListener(CheckAnswer);
        // Initially hide the wrong menu panel
        wrongText.gameObject.SetActive(false);

        // Store the original position of the input field
        inputFieldOriginalPosition = answerInput.transform.position;
    }

    public IEnumerator FadeText()
    {
        yield return new WaitForSeconds(1f);
        float fadeDuration = 1f; 
        float timer = 0f;

        Color startColor = wrongText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0); 

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            wrongText.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            yield return null;
        }
        wrongText.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        wrongText.gameObject.SetActive(false); 
    }

    private void CheckAnswer(string userAnswer)
    {
        // Find the game object with the name "Triangle"
        GameObject triangleObject = GameObject.Find("Triangle");
        // Get the sprite renderer attached to the triangleObject
        Image triangleRenderer = triangleObject.GetComponent<Image>();

        if (userAnswer.ToLower() == triangleRenderer.sprite.name.ToLower())
        {
            Debug.Log("Correct");
            // Hide the wrong menu panel
            wrongText.gameObject.SetActive(false);
            resourcesrandom.GetRandomResource();
            // Clear the input field
            answerInput.text = "";
            FindObjectOfType<Hint>().starthint = true;
            correctsound.Invoke();
        }
        else
        {
            Debug.Log("Wrong");
            wrongsound.Invoke();
            if (wrongText != null)
            {
                wrongText.gameObject.SetActive(true); 
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                }
                fadeCoroutine = StartCoroutine(FadeText());
            }
        }
    }

    private void Update()
    {
        // Check if the touch screen keyboard is visible
        if (TouchScreenKeyboard.visible)
        {
            // Move the input field above the keyboard
            Vector3 keyboardPosition = TouchScreenKeyboard.area.position;
            Vector3 inputFieldPosition = inputFieldOriginalPosition;
            inputFieldPosition.y = keyboardPosition.y - (answerInput.GetComponent<RectTransform>().rect.height / 2f);
            answerInput.transform.position = inputFieldPosition;
        }
        else
        {
            // Reset the input field position to its original position
            answerInput.transform.position = inputFieldOriginalPosition;
        }
    }
}

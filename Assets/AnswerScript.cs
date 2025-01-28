using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false, runGreen = false;
    public QuizManager quizManager;
    public GameObject wrongMenuPanel;
    public Image GameOverPanel;
    public UnityEvent wrongsound;
    public UnityEvent correctsound;
    public int wrongAnswers = 1;
    private int wrongIndex = -1;

    private Color wrongColor = new Color(0.859f, 0.071f, 0.071f, 0.61f); // Hexadecimal color DB1212
    private Color correctColor = new Color(0.584f, 0.824f, 0.216f, 0.61f); // Hexadecimal color 95D237

    void Update()
    {
        if (runGreen)
        {
            StartCoroutine(TurnGreen());
            runGreen = false;
        }
    }

    public void Answer()
    {
        if (!isCorrect)
        {
            Debug.Log("Wrong Answer");
            if (wrongIndex != -1)
            {
                quizManager.ResetPressedWrongAnswerColor(wrongIndex);
            }
            wrongIndex = transform.GetSiblingIndex();
            var image = GetComponent<Image>();
            image.color = wrongColor; 
            quizManager.wrongTimes++;
            wrongAnswers++;
            if (quizManager.wrongTimes == 1)
            {
                FindObjectOfType<PauseButton>().startreset = true;
                Debug.Log("GameOver: " + FindObjectOfType<PauseButton>().startreset);
                //if (quizManager.previousQuestion != quizManager.currentQuestion)
                //{
                    GameOverPanel.gameObject.SetActive(true);
                //}
            }
            else if (quizManager.wrongTimes == 3)quizManager.wrongTimes = 0;
            wrongsound.Invoke();
            if (quizManager.WrongMenuActive)
            {
                Debug.Log("blablablabla" + wrongAnswers.ToString());
            }
            else
            {
                wrongMenuPanel.SetActive(true);
                quizManager.WrongMenuActive = true;
            }
        }
        else
        {
            Debug.Log("Correct Answer");
            correctsound.Invoke();
            runGreen = true;
        }
    }

    IEnumerator TurnGreen()
    {
        var image = GetComponent<Image>();
        image.color = correctColor;
        
        yield return new WaitForSeconds(1f);
        quizManager.correct();
        quizManager.ResetColors();
    }
}

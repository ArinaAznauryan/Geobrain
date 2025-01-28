using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswer> QnA;
    public int wrongTimes = 0;
    public GameObject[] options;
    public bool sameQuestion = true;
    public int currentQuestion;
    public AdForReset adForReset;
    public int previousQuestion; // New variable for holding the index of the previous question
    public TextMeshProUGUI QuestionTxt;
    public TextMeshProUGUI QuestionCountTxt; // New variable for holding the reference to the UI text component
    public bool WrongMenuActive = false;
    private List<int> wrongIndices = new List<int>();
    private int pressedWrongAnswerIndex = -1;

    private Color grayColor = new Color(0.188f, 0.188f, 0.188f, 0.61f);
    private Color wrongColor = new Color(0.859f, 0.071f, 0.071f, 0.61f);
    private Color correctColor = new Color(0.584f, 0.824f, 0.216f, 0.61f);

    public int shownQuestionsCount; // Variable to keep track of the count of questions shown
    private int totalQuestions; // Variable to keep track of the total number of questions

    private void Start()
    {
        totalQuestions = QnA.Count; // Initialize totalQuestions with the count of QnA list
        generateQuestion();
        previousQuestion = currentQuestion; // Set previousQuestion initially to currentQuestion
        shownQuestionsCount = 1; // Initialize shownQuestionsCount to 1 since the first question is shown at the start
        UpdateQuestionCountUI(); // Update the UI with the current question count
    }

    public void correct()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
        FindObjectOfType<AnswerScript>().runGreen = false;
        wrongTimes = 0;
    }

    public void ResetColors()
    {
        foreach (var option in options)
        {
            var image = option.GetComponent<Image>();
            image.color = grayColor;
        }
        WrongMenuActive = false;
    }

    public void ResetPressedWrongAnswerColor(int index)
    {
        if (index == pressedWrongAnswerIndex)
        {
            var image = options[index].GetComponent<Image>();
            image.color = grayColor;
            pressedWrongAnswerIndex = -1;
        }
    }

    void SetAnswers()
    {
        wrongIndices.Clear();

        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            var image = options[i].GetComponent<Image>();
            image.color = grayColor;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i];
            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
            else
            {
                wrongIndices.Add(i);
            }
        }
    }

    void Update()
    {
        //Debug.Log("yomama: "+wrongTimes);
    }

    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            FindObjectOfType<PauseButton>().startreset = true;
            previousQuestion = currentQuestion; // Update previousQuestion before generating new question
            currentQuestion = Random.Range(0, QnA.Count);
            sameQuestion = false;
            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
            
            // Increment shownQuestionsCount only if a new question is shown
            shownQuestionsCount++; 
            UpdateQuestionCountUI(); // Update the UI with the current question count
        }
        else
        {
            LoadGoodJobScene();
        }
    }

    void LoadGoodJobScene()
    {
        if (shownQuestionsCount != QnA.Count) // Check if the count of shown questions has changed
        {
            SceneManager.LoadScene("Goodjob");
        }
        // Else, do nothing (disable game over panel usage)
    }

    void UpdateQuestionCountUI()
    {
        QuestionCountTxt.text = shownQuestionsCount + "/" + totalQuestions;
    }
}

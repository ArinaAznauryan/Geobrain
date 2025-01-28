using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionAndAnswer
{
    [SerializeField] public string Question;
    [SerializeField] public string[] Answers;
    [SerializeField] public int CorrectAnswer;
}
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Button[] btns;

    public QuestionList[] questions;

    public TMP_Text[] btnTxts;

    public TMP_Text qstnTxt;

    private QuestionList selectedQuestion;

    private static List<int> usedQuestions = new();


    private void Awake()
    {
        foreach (var btn in btns)
        {
            btn.GetComponent<SelectImageButton>().toggledOn += toggleOthersOff;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Eine random Frage auswählen und anzeigen
        int randomNumber = Random.Range(0, questions.Length);

        if (usedQuestions.Contains(randomNumber))
        {
            randomNumber = Random.Range(0, questions.Length);
        }

        selectedQuestion = questions[randomNumber];
        usedQuestions.Append(randomNumber);

        ShowQuestion(selectedQuestion);
    }

    private void toggleOthersOff(SelectImageButton button)
    {
        foreach(var btn in btns)
        {
            var selectBtn = btn.GetComponent<SelectImageButton>();

            if (selectBtn.IsSelected && !button.Equals(selectBtn))
            {
                selectBtn.ToggleButton();
            }
        }
    }

    private void ShowQuestion(QuestionList question)
    {
        qstnTxt.text = question.question;

        for (int i = 0; i < btnTxts.Length; i++)
        {
            btnTxts[i].text = question.answers[i];
        }
    }
    

    public void ValidateAnswer()
    {
        for(int i = 0;i < btns.Length; i++)
        {
            var button = btns[i].GetComponent<SelectImageButton>();

            if (button.IsSelected)
            {
                var isCorrect = i == selectedQuestion.correctIndex;
                GameManager.Instance.RaiseEvaluation(isCorrect ? 1 : -1);

                var controller = FindAnyObjectByType<CaptchaController>();
                if (controller) controller.LoadNextCaptcha(isCorrect);
            }
        }
    }
}

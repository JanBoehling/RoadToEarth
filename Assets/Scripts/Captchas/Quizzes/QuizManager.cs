using NUnit.Framework;
using TMPro;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    //public List<QuestionsAndAnswer> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public TextMeshProUGUI QuestionText;

    private void Start()
    {
        
    }

    private void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            //options[i].GetComponent<AnswerScript>().isCorrect = false;
            //options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].answers[i];


        }
    }

    private void generateQuestion()
    {
        //currentQuestion = Random.Range(0, QnA.count);

        //QuestionText.text = QnA[currentQuestion].question;
    }
}

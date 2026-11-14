using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fragenkatalog", menuName = "Quiz/Fragenauswahl")]
public class QuestionSet : ScriptableObject
{
    public List<QuestionData> questions = new List<QuestionData>();

    public QuestionData GetRandomQuestion()
    {
        return questions[Random .Range(0, questions.Count)];
    }
}

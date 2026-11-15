using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Scriptable Objects/QuestionList")]
public class QuestionList : ScriptableObject
{
    public string question;

    public string[] answers;

    public int correctIndex;
}

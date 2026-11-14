using UnityEngine;

[CreateAssetMenu(fileName = "Fragen", menuName = "Quiz/Fragen")]
public class QuestionData : ScriptableObject
{
    public string questionText = string.Empty;

    public string[] answers = new string[4];

    [Range (0f, 3f)]
    public int aiCorrectAnswerIndex;
}

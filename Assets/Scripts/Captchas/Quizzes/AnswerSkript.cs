using UnityEngine;

public class AnswerSkript : MonoBehaviour
{
    public bool isCorrect = false;

    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("Correct Answer");
        }
        else
        {
            Debug.Log("Wrong Answer");
        }
    }
}

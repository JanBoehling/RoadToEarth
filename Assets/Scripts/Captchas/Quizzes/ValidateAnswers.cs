using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ValidateAnswers : MonoBehaviour
{
    public GameObject aiAnswer;

    public GameObject correctAnswer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckAnswer()
    {
        GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        Debug.Log(clickedBtn);

        if (clickedBtn == correctAnswer || clickedBtn == aiAnswer)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Wrong");
        }
    }
}

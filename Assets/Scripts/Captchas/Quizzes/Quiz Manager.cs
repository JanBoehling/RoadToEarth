using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public GameObject[] questions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Alle Fragen deaktivieren
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].SetActive(false);
        }

        //Eine random Frage auswählen und anzeigen
        int randomNumber = Random.Range(0, questions.Length);
        questions[randomNumber].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // if the eval is negative, the "bad" ending is triggered
    public int Evaluation { get; private set; }

    [SerializeField] private Comments.Neutrality _currentNeutrality = Comments.Neutrality.Neutral;

    // TODO: set ending scene build indices
    private readonly Dictionary<Comments.Neutrality, int> _endingScenes = new()
    {
        { Comments.Neutrality.Positive, 1 },
        { Comments.Neutrality.Negative, 2 }
    };

    public void RaiseEvaluation(int value)
    {
        Evaluation += value;
    }

    /// <summary>
    /// Loads scene based on evaluation value<br/><br/>
    /// Bad ending if evaluation value is negative<br/>
    /// Good ending if evaluation value is 0 or positive
    /// </summary>
    public void EvaluateEnding()
    {
        if (Evaluation < 0) LoadScene(_endingScenes[Comments.Neutrality.Negative]);
        else LoadScene(_endingScenes[Comments.Neutrality.Positive]);
    }

    public void Quit() => Application.Quit();

    public void LoadScene(int buildIndex) => SceneManager.LoadScene(buildIndex);
    public void LoadNextScene() => LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}

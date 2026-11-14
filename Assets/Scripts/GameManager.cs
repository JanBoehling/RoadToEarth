using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    // if the eval is negative, the "bad" ending is triggered
    public int Evaluation { get; private set; }

    [SerializeField] private Comments.Neutrality _currentNeutrality = Comments.Neutrality.Neutral;

    private int _currentCaptchaIndex = 0;

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
        if (Evaluation < 0) LoadScene(SceneCollection.EndingScenes[Comments.Neutrality.Negative]);
        else LoadScene(SceneCollection.EndingScenes[Comments.Neutrality.Positive]);
    }

    /// <summary>
    /// Unloads the current showing captcha and loads the next one in the list
    /// </summary>
    public void AdvanceCaptcha() => LoadScene(SceneCollection.CaptchaScenes.ElementAt(++_currentCaptchaIndex).Value);

    public void Quit() => Application.Quit();

    public void LoadScene(int buildIndex, bool additive = false)
    {
        if (additive) SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()); // TODO: This might unload the base scene instead of the additively added one. Please test!

        SceneManager.LoadScene(buildIndex, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
    }

    public void LoadNextScene() => LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}

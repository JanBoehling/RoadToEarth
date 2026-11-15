using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoSingleton<GameManager>
{
    public InputActions Input { get; private set; }

    // if the eval is negative, the "bad" ending is triggered
    private int _evaluation;
    public int Evaluation
    {
        get => _evaluation;
        set
        {
            _evaluation = value;
            CurrentNeutrality = _evaluation < 0 ? Comments.Neutrality.Negative : Comments.Neutrality.Positive;
        }
    }

    public Comments.Neutrality CurrentNeutrality { get; private set; } = Comments.Neutrality.Neutral;

    private static readonly Dictionary<Comments.Neutrality, int> _endingScenes = new()
    {
        { Comments.Neutrality.Positive, 3 },
        { Comments.Neutrality.Negative, 4 }
    };

    protected override void Awake()
    {
        base.Awake();

        Input = new();
        Input.Default.Enable();
    }

    private void OnDestroy() => Input.Default.Disable();

    /// <summary>
    /// Adds given value to evaluation value
    /// </summary>
    /// <param name="value">-1 or +1</param>
    public void RaiseEvaluation(int value) => Evaluation += value;

    /// <summary>
    /// Loads scene based on evaluation value<br/><br/>
    /// Bad ending if evaluation value is negative<br/>
    /// Good ending if evaluation value is 0 or positive
    /// </summary>
    public void EvaluateEnding() => LoadScene(_endingScenes[CurrentNeutrality]);

    public void Quit() => Application.Quit();

    public void LoadScene(int buildIndex) => SceneManager.LoadScene(buildIndex);

    public void LoadNextScene() => LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    /// <summary>
    /// Danger: cursed fucking code
    /// </summary>
    public void ReturnToMainMenu()
    {
        // Destroying current GameManager so that the GameManager in the begining scene will not annihilate this one like some positron and electron
        Destroy(GameManager.Instance.gameObject);
        GameManager.Instance = null;

        // Destroying all AudioManagers so that there will be no background music AudioManager. I could also just destroy only that one but fuck it, scorched earth it is
        var audioPlayers = FindObjectsByType<AudioManager>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        for (int i = audioPlayers.Length - 1; i >= 0; i--)
        {
            Destroy(audioPlayers[i].gameObject);
        }

        LoadScene(0);
    }
}

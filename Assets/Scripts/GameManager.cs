using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoSingleton<GameManager>
{
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

    private int _currentCaptchaIndex = 0;

    /// <summary>
    /// Adds given value to evaluation value
    /// </summary>
    /// <param name="value">-1 or +1</param>
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
        LoadScene(SceneCollection.EndingScenes[CurrentNeutrality]);
    }

    /// <summary>
    /// Unloads the current showing captcha and loads the next one in the list
    /// </summary>
    public void AdvanceCaptcha()
    {
        if (++_currentCaptchaIndex >= SceneCollection.CaptchaScenes.Count)
        {
            EvaluateEnding();
            return;
        }

        LoadScene(SceneCollection.CaptchaScenes.ElementAt(_currentCaptchaIndex).Value);
    }

    public void Quit() => Application.Quit();

    public void LoadScene(int buildIndex, bool additive = false)
    {
        if (additive) SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()); // TODO: This might unload the base scene instead of the additively added one. Please test!

        SceneManager.LoadScene(buildIndex, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
    }

    public void LoadNextScene() => LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private GameManager _target;
    private void OnEnable() => _target = (GameManager)target;
    public override void OnInspectorGUI()
    {
        DrawPropertiesExcluding(serializedObject, "m_Script");
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField($"Current Neutrality: {_target.CurrentNeutrality}");
    }
}
#endif

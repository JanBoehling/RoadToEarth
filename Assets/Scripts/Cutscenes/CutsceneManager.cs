using System.Collections;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private float _musicFadeOutDurationOnStart = .5f;
    [SerializeField] private float _musicFadeInDurationOnEnd = .5f;

    private Animator _animator;

    // Skip settings
    private const float SkipButtonHoldTime = 3f;
    private float _timer = 0f;

    private void Awake()
    {
        _animator = transform.GetComponent<Animator>();
        if (!_animator) _animator = FindFirstObjectByType<Animator>();
        if (!_animator) Debug.LogWarning("Could not find animator component in scene!");
    }

    private void Start()
    {
        StartCoroutine(AutoContinueAfterAnimCO());
    }

    private void Update()
    {
        // Skip cutscene
        if (GameManager.Instance.Input.Default.SkipCutscene.IsPressed())
        {
            _timer += Time.deltaTime;
            if (_timer > SkipButtonHoldTime)
            {
                var audioManager = FindFirstObjectByType<AudioManager>();
                if (audioManager) audioManager.Resume(_musicFadeInDurationOnEnd);
                LoadNextScene();
            }
        }
        else _timer = 0f;
    }

    private IEnumerator AutoContinueAfterAnimCO()
    {
        var audioManager = FindFirstObjectByType<AudioManager>();
        if (audioManager) audioManager.Pause(_musicFadeOutDurationOnStart);

        float animationDuration = _animator.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log($"Waiting for {animationDuration} seconds");

        yield return new WaitForSeconds(animationDuration);

        if (audioManager) audioManager.Resume(_musicFadeInDurationOnEnd);
        LoadNextScene();
    }

    public void LoadNextScene() => GameManager.Instance.LoadNextScene();
}

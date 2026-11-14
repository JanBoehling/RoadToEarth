using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private bool _autoPlay = true;

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
        if (_autoPlay) StartCoroutine(AutoPlayAfterAnimCO());
    }

    private void Update()
    {
        // Skip cutscene
        if (GameManager.Instance.Input.Default.SkipCutscene.IsPressed())
        {
            _timer += Time.deltaTime;
            if (_timer > SkipButtonHoldTime) LoadNextScene();
        }
        else _timer = 0f;
    }

    private IEnumerator AutoPlayAfterAnimCO()
    {
        var audioManager = FindFirstObjectByType<AudioManager>();
        if (audioManager) audioManager.Pause();
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        if (audioManager) audioManager.Resume();
        LoadNextScene();
    }

    public void LoadNextScene() => GameManager.Instance.LoadNextScene();
}

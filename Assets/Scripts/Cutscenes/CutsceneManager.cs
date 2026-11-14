using System.Collections;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private bool _autoPlay = true;

    private Animator _animator;

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

    private IEnumerator AutoPlayAfterAnimCO()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        LoadNextScene();
    }

    public void LoadNextScene() => GameManager.Instance.LoadNextScene();
}

using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CleanupCaptcha : MonoBehaviour
{
    [SerializeField] private CleanupItem[] _primaryObjects;
    [SerializeField] private CleanupItem[] _secondaryObjects;
    [SerializeField] private CleanupItem[] _tertiaryObjects;
    [Space]
    [SerializeField] private TMP_Text _promptTextBox;
    [SerializeField, TextArea(1, 3)] private string _promptText;
    [SerializeField] private float _promptShowDuration = 3f;
    [Space]
    [SerializeField] private float _endWaitDuration = 5f;
    [Space]
    [SerializeField] private AudioClip _validateSFX;

    private int _objectsDestroyed = 0;

    private void Start()
    {
        ToggleObjectInteraction(_secondaryObjects, false);
        ToggleObjectInteraction(_tertiaryObjects, false);

        if (_promptTextBox) _promptTextBox.text = _promptText; //StartCoroutine(DisplayPromptText());
    }

    public void DestroyObject(GameObject obj)
    {
        Debug.Log($"Destroying object {obj.name}");
        Destroy(obj);
        _objectsDestroyed++;

        if (_objectsDestroyed == _primaryObjects.Length + 1) ToggleObjectInteraction(_secondaryObjects, true);

        if (_objectsDestroyed == _primaryObjects.Length + 1 + _secondaryObjects.Length) ToggleObjectInteraction(_tertiaryObjects, true);

        if (_objectsDestroyed == _primaryObjects.Length + 1 + _secondaryObjects.Length + _tertiaryObjects.Length)
        {
            StartCoroutine(EndWaitDelay(_endWaitDuration, EndCaptcha));
        }
    }

    private void EndCaptcha()
    {
        CreateVeryDirtyAudioPlayerForValidateSFX();

        var controller = FindAnyObjectByType<CaptchaController>();
        if (controller) controller.LoadNextCaptcha(true);
        else Debug.LogWarning("Count not find CaptchaController in scene!");
    }

    private void CreateVeryDirtyAudioPlayerForValidateSFX()
    {
        var player = new GameObject("Very dirty audio player that will soon be destroyed. Please don't mind me", typeof(AudioManager)).GetComponent<AudioManager>();
        player.Play(_validateSFX);
        DontDestroyOnLoad(player);
        Destroy(player, _validateSFX.length + 1f);
    }

    private void ToggleObjectInteraction(CleanupItem[] items, bool value)
    {
        foreach (var item in items)
        {
            item.ButtonComponent.interactable = value;
        }
    }

    private IEnumerator DisplayPromptText()
    {
        _promptTextBox.text = _promptText;
        yield return new WaitForSeconds(_promptShowDuration);
        _promptTextBox.gameObject.SetActive(false);
    }

    private IEnumerator EndWaitDelay(float waitDelayInSeconds, System.Action action)
    {
        yield return new WaitForSeconds(waitDelayInSeconds);
        action?.Invoke();
    }
}

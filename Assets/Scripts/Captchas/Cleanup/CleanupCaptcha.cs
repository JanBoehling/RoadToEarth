using System.Collections;
using TMPro;
using UnityEngine;

public class CleanupCaptcha : MonoBehaviour
{
    [SerializeField] private CleanupItem[] _primaryObjects;
    [SerializeField] private CleanupItem[] _secondaryObjects;
    [SerializeField] private CleanupItem[] _tertiaryObjects;

    [SerializeField] private TMP_Text _promptTextBox;
    [SerializeField, TextArea(1, 3)] private string _promptText;
    [SerializeField] private float _promptShowDuration = 3f;

    [SerializeField] private float _endWaitDuration = 5f;

    private int _objectsDestroyed = 0;

    private void Start()
    {
        ToggleObjectInteraction(_secondaryObjects, false);
        ToggleObjectInteraction(_tertiaryObjects, false);

        if (_promptTextBox) _promptTextBox.text = _promptText; //StartCoroutine(DisplayPromptText());
    }

    public void DestroyObject(GameObject obj)
    {
        Destroy(obj);
        _objectsDestroyed++;

        if (_objectsDestroyed == _primaryObjects.Length) ToggleObjectInteraction(_secondaryObjects, true);

        if (_objectsDestroyed == _primaryObjects.Length + _secondaryObjects.Length) ToggleObjectInteraction(_tertiaryObjects, true);

        if (_objectsDestroyed == _primaryObjects.Length + _secondaryObjects.Length + _tertiaryObjects.Length)
        {
            StartCoroutine(EndWaitDelay(_endWaitDuration, EndCaptcha));
        }
    }

    private void EndCaptcha()
    {
        var controller = FindAnyObjectByType<CaptchaController>();
        if (controller) controller.LoadNextCaptcha();
        else Debug.LogWarning("Count not find CaptchaController in scene!");
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

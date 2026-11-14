using UnityEngine;

public class CaptchaController : MonoBehaviour
{
    [SerializeField] private GameObject[] _captchaPrefabs;
    [SerializeField] private Transform _captchaContainer;

    private int _currentCaptchaIndex = -1;

    private void Start() => InstantiateCaptchaPrefab();

    /// <summary>
    /// Destroys current captcha prefab and instantiates the next one in the list<br/>
    /// If the last captcha was instantiated, the next scene will be loaded
    /// </summary>
    public void LoadNextCaptcha()
    {
        if (_currentCaptchaIndex >= _captchaPrefabs.Length) GameManager.Instance.LoadNextScene();

        DestroyChild();
        InstantiateCaptchaPrefab();
    }

    private void InstantiateCaptchaPrefab() => Instantiate(_captchaPrefabs[++_currentCaptchaIndex], _captchaContainer);

    private void DestroyChild() => Destroy(_captchaContainer.GetChild(0).gameObject);
}

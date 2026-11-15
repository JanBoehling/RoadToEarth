using System.Collections;
using UnityEngine;

public class CaptchaController : MonoBehaviour
{
    [SerializeField] private GameObject[] _captchaPrefabs;
    [SerializeField] private Transform _captchaContainer;
    [Space]
    [SerializeField] private float _riseAnimationSpeed = 400f;
    [SerializeField] private float _swivelMagnitude = 10f;
    [SerializeField] private RectTransform _angel;
    [SerializeField] private AudioSet _angelAudioClips;
    [SerializeField] private RectTransform _demon;
    [SerializeField] private AudioSet _demonAudioClips;
    [SerializeField] private AudioManager _riseEffectAudioManager;

    private int _currentCaptchaIndex = -1;

    private void Start() => InstantiateCaptchaPrefab();

    /// <summary>
    /// Destroys current captcha prefab and instantiates the next one in the list<br/>
    /// If the last captcha was instantiated, the next scene will be loaded
    /// </summary>
    /// <param name="success">If the validation was successfull (good). Determines if angel(good/true) or baby(bad/false) rises</param>
    public void LoadNextCaptcha(bool success) => StartCoroutine(LoadNextCaptchaCO(success));

    private void InstantiateCaptchaPrefab() => Instantiate(_captchaPrefabs[++_currentCaptchaIndex], _captchaContainer);

    private void DestroyChild() => Destroy(_captchaContainer.GetChild(0).gameObject);

    private IEnumerator LoadNextCaptchaCO(bool success)
    {
        yield return PlayDemonAngelAnimCO(success ? _angel : _demon, success ? _angelAudioClips.GetRandom() : _demonAudioClips.GetRandom());

        // End game if all captchas are done
        if (_currentCaptchaIndex >= _captchaPrefabs.Length - 1)
        {
            GameManager.Instance.EvaluateEnding();
            yield break;
        }
        
        // Load next captcha
        DestroyChild();
        InstantiateCaptchaPrefab();
    }

    private IEnumerator PlayDemonAngelAnimCO(RectTransform obj, AudioClip sfx)
    {
        obj.gameObject.SetActive(true);

        var originalPosition = obj.anchoredPosition;
        var cameraUpperBound = obj.GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta.y * .5f + obj.sizeDelta.y * .5f;
        Debug.Log($"Canvas size: {obj.GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta}\nCamUpBound: {cameraUpperBound}");

        if (sfx && _riseEffectAudioManager) _riseEffectAudioManager.Play(sfx);

        while (obj.anchoredPosition.y < cameraUpperBound)
        {
            // Rise animation
            obj.anchoredPosition += _riseAnimationSpeed * Time.deltaTime * Vector2.up;

            // Swivel effect
            obj.anchoredPosition += _swivelMagnitude * Random.Range(-1f, 1f) * Vector2.right;

            yield return null;
        }

        if(sfx && _riseEffectAudioManager) _riseEffectAudioManager.Stop();

        obj.anchoredPosition = originalPosition;
        obj.gameObject.SetActive(false);
    }
}

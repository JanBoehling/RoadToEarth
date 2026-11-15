using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private bool _isPersistent = false;
    [SerializeField] private bool _playOnAwake = false;
    [SerializeField] private bool _isLooping = false;
    [SerializeField, Range(0f, 1f)] private float _volume = 1f;

    [SerializeField, Tooltip("Optional: local audio clip")] private AudioClip _localClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();

        _audioSource.playOnAwake = _playOnAwake;
        if (_localClip) _audioSource.clip = _localClip;
        _audioSource.loop = _isLooping;
        _audioSource.volume = _volume;

        if (_isPersistent) DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (_playOnAwake && _localClip) _audioSource.Play();
    }

    public void PlayLocalClip() => _audioSource.Play();
    public void Play(AudioClip clip)
    {
        _audioSource.clip = clip;
        PlayLocalClip();
    }
    public void PlayRandomFromSet(AudioSet audioSet)
    {
        Play(audioSet.Set[Random.Range(0, audioSet.Length)]);
    }
    public void Pause(float fadeDuration)
    {
        if (_audioSource.clip) StartCoroutine(PauseCO(fadeDuration));
    }
    public void Pause() => Pause(0);
    public void Resume(float fadeDuration)
    {
        if (_audioSource.clip) StartCoroutine(ResumeCO(fadeDuration));
    }
    public void Resume() => Resume(0);
    public void Stop()
    {
        if (_audioSource.clip) _audioSource.Stop();
    }
    public void SetVolume(float volume) => _audioSource.volume = volume;

    private IEnumerator PauseCO(float fadeDuration)
    {
        if (fadeDuration == 0f)
        {
            _audioSource.Pause();
            yield break;
        }

        float volume = _audioSource.volume;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(volume, 0f, timer / fadeDuration);
            yield return null;
        }
        _audioSource.Pause();
        _audioSource.volume = volume;
    }
    private IEnumerator ResumeCO(float fadeDuration)
    {
        if (fadeDuration == 0f)
        {
            _audioSource.UnPause();
            yield break;
        }

        float volume = _audioSource.volume;
        float timer = 0f;

        _audioSource.volume = 0f;
        _audioSource.UnPause();

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(0f, volume, timer / fadeDuration);
            yield return null;
        }
        _audioSource.volume = volume;
    }
}

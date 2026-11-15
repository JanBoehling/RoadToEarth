using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private bool _isPersistent = false;
    [SerializeField] private bool _playOnAwake = false;
    [SerializeField] private bool _isLooping = false;

    [SerializeField, Tooltip("Optional: local audio clip")] private AudioClip _localClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();

        _audioSource.playOnAwake = _playOnAwake;
        if (_localClip) _audioSource.clip = _localClip;
        _audioSource.loop = _isLooping;

        if (_isPersistent) DontDestroyOnLoad(gameObject);
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
    public void Pause()
    {
        if (_audioSource.clip) _audioSource.Pause();
    }
    public void Resume()
    {
        if (_audioSource.clip) _audioSource.UnPause();
    }
    public void Stop()
    {
        if (_audioSource.clip) _audioSource.Stop();
    }
}

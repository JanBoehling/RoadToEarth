using UnityEngine;

public class InactivityMonitor : MonoSingleton<InactivityMonitor>
{
    private const float InactivityThreshold = 60f;
    private float _timer = 0f;

    [SerializeField] private AudioClip[] _inactivityVoicelines;
    private AudioManager _voicelineAudioManager;

    protected override void Awake()
    {
        base.Awake();
        _voicelineAudioManager = GetComponentInChildren<AudioManager>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (GameManager.Instance.Input.Default.AnyKey.WasReleasedThisFrame()) _timer = 0f;

        if (!CheckForInactivity()) return;

        DisplayInactivityMessage(Comments.GetRandomInactivityComment());
        _timer = 0f;
    }

    // TODO
    private void DisplayInactivityMessage(string msg)
    {
        Debug.Log(msg);
        if (_inactivityVoicelines.Length > 0) _voicelineAudioManager.Play(_inactivityVoicelines[Random.Range(0, _inactivityVoicelines.Length)]);
    }

    public bool CheckForInactivity() => _timer > InactivityThreshold;
}

using UnityEngine;

public class InactivityMonitor : MonoSingleton<InactivityMonitor>
{
    [SerializeField] private float _inactivityThreshold = 60f;
    [SerializeField] private AudioSet _inactivityVoicelines;

    private AudioManager _voicelineAudioManager;
    private float _timer = 0f;

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
        if (_inactivityVoicelines && _inactivityVoicelines.Length > 0) _voicelineAudioManager.Play(_inactivityVoicelines.Set[Random.Range(0, _inactivityVoicelines.Length)]);
    }

    public bool CheckForInactivity() => _timer > _inactivityThreshold;
}

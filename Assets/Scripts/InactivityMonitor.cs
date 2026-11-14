using UnityEngine;

public class InactivityMonitor : MonoBehaviour
{
    private const float InactivityThreshold = 60f;
    private float _timer = 0f;

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
    }

    public bool CheckForInactivity() => _timer > InactivityThreshold;
}

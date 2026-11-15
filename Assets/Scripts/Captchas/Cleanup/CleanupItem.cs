using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CleanupItem : MonoBehaviour
{
    public Button ButtonComponent { get; private set; }
    private CleanupCaptcha _controller;

    private void Awake()
    {
        ButtonComponent = GetComponent<Button>();
        _controller = FindAnyObjectByType<CleanupCaptcha>();
    }

    private void Start()
    {
        ButtonComponent.onClick.AddListener(OnClickAction);
    }

    private void OnClickAction()
    {
        Debug.Log("OnClickAction");
        _controller.DestroyObject(gameObject);
    }
}

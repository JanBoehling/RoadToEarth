using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectImageButton : MonoBehaviour
{
    private const float SelectedAlpha = .8f;

    [field: SerializeField] public bool IsSelected { get; private set; }

    private Image _img;

    private void Awake() => _img = GetComponent<Image>();

    public void ToggleButton()
    {
        IsSelected = !IsSelected;
        _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, IsSelected ? SelectedAlpha : 1f);
    }
}

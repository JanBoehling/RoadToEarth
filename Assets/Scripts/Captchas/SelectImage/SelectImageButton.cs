using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class SelectImageButton : MonoBehaviour
{
    private const float SelectedAlpha = .8f;

    [field: SerializeField] public bool IsCorrect { get; private set; }
    [field: SerializeField] public bool IsSelected { get; private set; }

    public Image ImageComponent {get; private set;}
    public Button ButtonComponent { get; private set; }

    private void Awake()
    {
        ImageComponent = GetComponent<Image>();
        ButtonComponent = GetComponent<Button>();
    }

    public void ToggleButton()
    {
        IsSelected = !IsSelected;
        ImageComponent.color = new Color(ImageComponent.color.r, ImageComponent.color.g, ImageComponent.color.b, IsSelected ? SelectedAlpha : 1f);
    }
}

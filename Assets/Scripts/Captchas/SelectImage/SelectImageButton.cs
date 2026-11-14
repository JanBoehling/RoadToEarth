using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class SelectImageButton : MonoBehaviour
{
    private Color SelectedColor = new Color(0f, 0f, 1f, 0.5f);
    private Color UnselectedColor = new Color(1f, 1f, 1f, 1f);

    [field: SerializeField] public bool IsSelected { get; private set; }

    public Image ImageComponent {get; private set;}
    public Button ButtonComponent { get; private set; }

    private void Awake()
    {
        ImageComponent = GetComponent<Image>();
        ButtonComponent = GetComponent<Button>();
        ButtonComponent.onClick.AddListener(ToggleButton);
    }

    public void ToggleButton()
    {
        IsSelected = !IsSelected;

        if (IsSelected)
        {
            ImageComponent.color = SelectedColor;
        }
        else
        {
            ImageComponent.color = UnselectedColor;
        }
    }
}

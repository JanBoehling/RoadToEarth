using UnityEngine;
using UnityEngine.UI;

public class DisableAllButtons : MonoBehaviour
{
    [SerializeField] private GameObject _parent;

    public void DisableAllInteractables()
    {
        var interactables = _parent.GetComponentsInChildren<Selectable>();
        foreach (var interactable in interactables) interactable.interactable = false;
    }
}

using UnityEngine;

public class SpecialEndingComponent : MonoBehaviour
{
    public void UnlockSpecialEnding()
    {
        GameManager.Instance.LoadScene(4);
    }
}

using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    public void ReturnToMainMenu() => GameManager.Instance.ReturnToMainMenu();
    public void Quit() => GameManager.Instance.Quit();
}

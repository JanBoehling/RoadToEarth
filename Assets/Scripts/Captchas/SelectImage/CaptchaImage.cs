using UnityEngine;
[System.Serializable]
public class CaptchaImage
{
    [SerializeField] public UnityEngine.UI.Image ImageComponent;
    [SerializeField] public Sprite Img;
    [SerializeField] public bool IsCorrect;
}

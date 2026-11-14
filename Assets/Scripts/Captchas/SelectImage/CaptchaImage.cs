using UnityEngine;
[System.Serializable]
public class CaptchaImage
{
    [SerializeField] public SelectImageButton ButtonObject;
    [SerializeField] public Sprite Img;
    [SerializeField] public bool IsCorrect;
}

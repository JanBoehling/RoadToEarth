using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectImageCaptcha : Captcha
{
    //// This should be a scriptable object to work, i think
    //private CaptchaImage[,] _captchaImages = new CaptchaImage[3, 3];
    //public CaptchaImage[,] CaptchaImages
    //{
    //    get => _captchaImages;
    //    set => _captchaImages = value;
    //}

    [SerializeField] private List<SelectImageButton> _buttons;

    public override bool Check()
    {
        // Disable all buttons
        foreach (var button in _buttons) button.GetComponent<Button>().interactable = false;

        int failCount = 0;
        const int FailThreshold = 2;

        // Count each incorrect input
        foreach (var button in _buttons) if ((button.IsSelected && !button.IsCorrect) || (!button.IsSelected && button.IsCorrect)) failCount++;

        Debug.Log(failCount);
        return failCount > FailThreshold;
    }

    public void DoCheck() => Check();

    public void ResetScene() => UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
}

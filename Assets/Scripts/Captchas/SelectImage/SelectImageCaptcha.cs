using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectImageCaptcha : Captcha
{
#if true
    // This should be a scriptable object to work, i think
    [SerializeField] public List<CaptchaImage> CaptchaImages;
#endif

    [SerializeField] private List<SelectImageButton> _buttons;

    private void Start()
    {
        foreach (var item in CaptchaImages)
        {
            item.ImageComponent.sprite = item.Img;
        }

        string test = "";
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                test += $"{CaptchaImages[j + 3 * i].IsCorrect} ";
            }
            test += '\n';
        }
        Debug.Log(test);   
    }

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

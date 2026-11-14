using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectImageCaptcha : Captcha
{
#if true
    // This should be a scriptable object to work, i think
    [SerializeField, HideInInspector] public List<CaptchaImage> CaptchaImages;
#endif

    private void Start()
    {
        // Apply sprites to canvas objects
        for (int y = 0; y < 3; y++)
        {
            for(int x = 0; x < 3; x++)
            {
                var button = CaptchaImages[GetIndex1D(x, y, 3)];
                button.ButtonObject.ImageComponent.sprite = button.Img;
            }
        }
    }

    public override bool Check()
    {
        // Disable all buttons
        foreach (var button in CaptchaImages) button.ButtonObject.ButtonComponent.interactable = false;

        int failCount = 0;
        const int FailThreshold = 2;

        // Count each incorrect input
        foreach (var button in CaptchaImages) if ((button.ButtonObject.IsSelected && !button.IsCorrect) || (!button.ButtonObject.IsSelected && button.IsCorrect)) failCount++;

        Debug.Log(failCount);
        return failCount > FailThreshold;
    }

    public static int GetIndex1D(int x, int y, int rowCount) => y + rowCount * x;

    public void DoCheck() => Check();

    public void ResetScene() => UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

    private void DebugLogCorrectImages()
    {
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
}

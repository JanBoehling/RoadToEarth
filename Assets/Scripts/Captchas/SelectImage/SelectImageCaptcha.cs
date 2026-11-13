using UnityEngine;

public class SelectImageCaptcha : Captcha
{
    private CaptchaImage[,] _captchaImages = new CaptchaImage[3, 3];
    public CaptchaImage[,] CaptchaImages
    {
        get => _captchaImages;
        set => _captchaImages = value;
    }

    public override bool Check()
    {
        throw new System.NotImplementedException();
    }
}

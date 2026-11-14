using System.Collections.Generic;

public static class SceneCollection
{
    // TODO: set ending scene build indices
    public static readonly Dictionary<Comments.Neutrality, int> EndingScenes = new()
    {
        { Comments.Neutrality.Positive, 1 },
        { Comments.Neutrality.Negative, 2 }
    };

    // TODO: set captcha scene build indices
    public static readonly Dictionary<CaptchaScenes, int> CaptchaScenes = new()
    {
        { global::CaptchaScenes.ShapeGen, 1 },
        { global::CaptchaScenes.SelectImage, 2 },
        { global::CaptchaScenes.Cleanup, 3 }
    };
}

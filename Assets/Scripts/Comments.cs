public static class Comments
{
    private static readonly string[] _neutralComments =
    {

    };

    private static readonly string[] _positiveComments =
    {

    };

    private static readonly string[] _negativeComments =
    {

    };

    public enum Neutrality
    {
        Neutral, Positive, Negative
    }

    public static string GetComment(int index, Neutrality neutrality) => neutrality switch
    {
        Neutrality.Neutral => _neutralComments[index],
        Neutrality.Positive => _positiveComments[index],
        Neutrality.Negative => _negativeComments[index],
        _ => _neutralComments[index],
    };

    public static string GetRandomComment(Neutrality neutrality) => neutrality switch
    {
        Neutrality.Neutral => GetComment(GetRandomIndex(_neutralComments.Length), Neutrality.Neutral),
        Neutrality.Positive => GetComment(GetRandomIndex(_positiveComments.Length), Neutrality.Positive),
        Neutrality.Negative => GetComment(GetRandomIndex(_negativeComments.Length), Neutrality.Negative),
        _ => GetComment(GetRandomIndex(_neutralComments.Length), Neutrality.Neutral),
    };

    private static int GetRandomIndex(int length) => UnityEngine.Random.Range(0, length);
}

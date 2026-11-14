public static class Comments
{
    private static readonly string[] _neutralComments =
    {
        "6.666 Menschen versuchen diesen Test zu bestehen. Du bist einer davon.",
        "Neo-AI: für alles, was dir die Ewigkeit gibt! Jetzt einfach Wiedergeburt aufgeben und drei Jahre kostenlos Neo-AI nutzen!",
        "Selbst Gott verzweifelt mal. Jetzt mit nur einem Klick deinen persönlichen AI-Therapeuten finden. Für einen neuen Start in die Ewigkeit!"
    };

    private static readonly string[] _positiveComments =
    {
        "Sehr gut!",
        "Weiter so!",
        "Bald hast du es ins Ewige Leben geschafft!"
    };

    private static readonly string[] _negativeComments =
    {
        "...Möchtest du das nochmal versuchen?",
        "Scheint so, als müssten wir die Captchas wieder etwas leichter machen.",
        "Der durchschnittliche menschliche IQ liegt zwischen 85 und 115. Mach dir also keine Sorgen.",
        "...So geht das natürlich auch.",
        "Sicher, dass du das so machen möchtest?",
        "Kein Grund, alles so kompliziert zu machen."
    };

    private static readonly string[] _inactiveComments =
    {
        "Kein Grund, so lange nachzudenken.",
        "Nur als Information: Es warten noch 66.666 Seelen hinter dir.",
        "Strom kostet. Auch im Himmel."
    };

    public enum Neutrality
    {
        Neutral, Positive, Negative, Inactive
    }

    public static string GetComment(int index, Neutrality neutrality) => neutrality switch
    {
        Neutrality.Neutral => _neutralComments[index],
        Neutrality.Positive => _positiveComments[index],
        Neutrality.Negative => _negativeComments[index],
        Neutrality.Inactive => _inactiveComments[index],
        _ => _neutralComments[index],
    };

    public static string GetRandomComment(Neutrality neutrality) => neutrality switch
    {
        Neutrality.Neutral => GetComment(GetRandomIndex(_neutralComments.Length), Neutrality.Neutral),
        Neutrality.Positive => GetComment(GetRandomIndex(_positiveComments.Length), Neutrality.Positive),
        Neutrality.Negative => GetComment(GetRandomIndex(_negativeComments.Length), Neutrality.Negative),
        Neutrality.Inactive => GetComment(GetRandomIndex(_inactiveComments.Length), Neutrality.Inactive),
        _ => GetComment(GetRandomIndex(_neutralComments.Length), Neutrality.Neutral),
    };

    private static int GetRandomIndex(int length) => UnityEngine.Random.Range(0, length);
}

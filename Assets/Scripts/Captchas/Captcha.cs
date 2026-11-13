using UnityEngine;

public abstract class Captcha : MonoBehaviour
{
    public abstract bool Check();
}

/* oder lieber abstract?
public interface ICaptcha
{
    public bool Check();
}*/

using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(SelectImageCaptcha))]
public class SelectImageCaptchaEditor : Editor
{
    private SelectImageCaptcha _target;

    private GUISkin _skin;

    (int width, int height) _captchaImageAmount;

    private enum CurrentContext
    {
        Sprite, Config, Ref
    }
    private CurrentContext _currentContext;
    private System.Collections.Generic.Dictionary<CurrentContext, GUIContent> _contextItems;

    private void OnEnable()
    {
        _target = (SelectImageCaptcha)target;
        _skin = CreateInstance<GUISkin>();
        _captchaImageAmount = (_target.CaptchaImages.GetLength(0), _target.CaptchaImages.GetLength(1));
        _contextItems = new()
        {
            { CurrentContext.Sprite, EditorGUIUtility.IconContent("Packages/com.unity.render-pipelines.core/Editor/LookDev/Icons/d_Environment@2x.png") },
            { CurrentContext.Config, EditorGUIUtility.IconContent("Packages/com.unity.2d.tilemap/Editor/Icons/d_GridSelection@2x.png") },
            { CurrentContext.Ref, EditorGUIUtility.IconContent("Packages/com.unity.multiplayer.center/Editor/MultiplayerCenterWindow/UI/Icons/d_PackageManager@2x.png") },
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "m_Script");
        
        switch (_currentContext)
        {
            case CurrentContext.Sprite:
                DrawSpriteContext(_captchaImageAmount.width, _captchaImageAmount.height);
                break;
            case CurrentContext.Config:
                DrawConfigContext(_captchaImageAmount.width, _captchaImageAmount.height);
                break;
            case CurrentContext.Ref:
                DrawRefContext(_captchaImageAmount.width, _captchaImageAmount.height);
                break;
            default:
                DrawSpriteContext(_captchaImageAmount.width, _captchaImageAmount.height);
                break;
        }

        EditorGUILayout.Separator();

        EditorGUILayout.BeginHorizontal();
        for (int i = 0; i < _contextItems.Count; i++)
        {
            var guiContent = _contextItems.ElementAt(i).Value;
            guiContent.tooltip = _contextItems.ElementAt(i).Key.ToString();
            if (GUILayout.Button(guiContent, SetWidthHeight())) _currentContext = _contextItems.ElementAt(i).Key;
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawGrid(int width, int height, System.Action<int, int> editorGUILayoutField)
    {
        for (int x = 0; x < width; x++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int y = 0; y < height; y++)
            {
                editorGUILayoutField?.Invoke(x, y);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void DrawSpriteContext(int width, int height) => DrawGrid(width, height, (i, j) => _target.CaptchaImages[i, j].Img = EditorGUILayout.ObjectField(_target.CaptchaImages[i, j].Img, typeof(Sprite), false, SetWidthHeight()) as Sprite);

    private void DrawConfigContext(int width, int height) => DrawGrid(width, height, (i, j) => _target.CaptchaImages[i, j].IsCorrect = GUILayout.Toggle(_target.CaptchaImages[i, j].IsCorrect, "", _skin.button, SetWidthHeight(65.2f, 65.2f))); // Random-ass magic number to match size of object fields

    private void DrawRefContext(int width, int height) => DrawGrid(width, height, (i, j) => _target.CaptchaImages[i, j].ImageComponent = EditorGUILayout.ObjectField(_target.CaptchaImages[i, j].ImageComponent, typeof(UnityEngine.UI.Image), true, SetWidthHeight()) as UnityEngine.UI.Image);

    private GUILayoutOption[] SetWidthHeight(float width = 64, float height = 64) => new GUILayoutOption[] { GUILayout.Width(width), GUILayout.Height(height) };
    private GUILayoutOption SetWidth(float width = 64) => GUILayout.Width(width);
    private GUILayoutOption SetHeight(float height = 64) => GUILayout.Height(height);
}

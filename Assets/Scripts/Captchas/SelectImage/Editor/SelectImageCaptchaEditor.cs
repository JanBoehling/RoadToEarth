using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SelectImageCaptcha))]
public class SelectImageCaptchaEditor : Editor
{
    private SelectImageCaptcha _target;

    private void OnEnable() => _target = (SelectImageCaptcha)target;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "m_Script");

        (int width, int height) = (_target.CaptchaImages.GetLength(0), _target.CaptchaImages.GetLength(1));
        DrawGrid(width, height, (i, j) => _target.CaptchaImages[i, j].Img = EditorGUILayout.ObjectField(_target.CaptchaImages[i, j].Img, typeof(Sprite), false, GUILayout.Width(64), GUILayout.Height(64)) as Sprite);

        
        DrawGrid(width, height, (i, j) => _target.CaptchaImages[i, j].IsCorrect = EditorGUILayout.Toggle(_target.CaptchaImages[i, j].IsCorrect));
        
        DrawGrid(width, height, (i, j) => _target.CaptchaImages[i, j].ImageComponent = EditorGUILayout.ObjectField(_target.CaptchaImages[i, j].ImageComponent, typeof(UnityEngine.UI.Image), true) as UnityEngine.UI.Image);

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
}

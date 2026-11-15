using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private GameManager _target;
    private void OnEnable() => _target = (GameManager)target;
    public override void OnInspectorGUI()
    {
        DrawPropertiesExcluding(serializedObject, "m_Script");
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField($"Current Neutrality: {_target.CurrentNeutrality}");
    }
}

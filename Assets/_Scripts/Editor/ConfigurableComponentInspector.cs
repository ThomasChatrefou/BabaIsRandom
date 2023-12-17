using NaughtyAttributes.Editor;
using System.Linq;
using UnityEditor;

public class ConfigurableComponentInspector : NaughtyInspector
{
    private WorldConfig _config;
    private bool _fold = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        string propName = (serializedObject.targetObject as IConfigurableComponent).GetConfigPropertyName();
        _config = (WorldConfig)serializedObject.FindProperty(propName).objectReferenceValue;

        _fold = EditorGUILayout.BeginFoldoutHeaderGroup(_fold, "Open Config");
        if (_fold)
        {
            SerializedObject so = new(_config);
            DrawNaughtyPropertiesExcluding(so, "m_Script");
            so.ApplyModifiedProperties();
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawNaughtyPropertiesExcluding(SerializedObject obj, params string[] propertyToExclude)
    {
        SerializedProperty iterator = obj.GetIterator();
        bool enterChildren = true;
        while (iterator.NextVisible(enterChildren))
        {
            enterChildren = false;
            if (!propertyToExclude.Contains(iterator.name))
            {
                NaughtyEditorGUI.PropertyField_Layout(iterator, true);
            }
        }
    }
}
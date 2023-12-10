using System.Linq;
using NaughtyAttributes.Editor;
using UnityEditor;

[CustomEditor(typeof(GridHandlerComponent))]
public class GridHandlerComponentInspector : NaughtyInspector
{
    private GridConfig _gridConfig;
    private bool _fold = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        _gridConfig = (GridConfig)serializedObject.FindProperty("_gridConfig").objectReferenceValue;

        _fold = EditorGUILayout.BeginFoldoutHeaderGroup(_fold, "Open Config");
        if (_fold)
        {
            SerializedObject so = new(_gridConfig);
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
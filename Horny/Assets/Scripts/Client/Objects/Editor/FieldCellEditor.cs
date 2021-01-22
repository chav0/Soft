using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Client.Objects
{
    [CustomEditor(typeof(FieldCell))]
    [CanEditMultipleObjects]
    public class FieldCellEditor : Editor
    {
        private FieldCell _fieldCell;

        protected void OnEnable()
        {
            _fieldCell = (FieldCell) target;
            _fieldCell.OnEnable();
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUI.changed)
            {
                _fieldCell.Colorize(_fieldCell.State);
                EditorUtility.SetDirty(_fieldCell);
                EditorSceneManager.MarkSceneDirty(_fieldCell.gameObject.scene);
            }
        }
    }
}
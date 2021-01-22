using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Client.Objects
{
    [CustomEditor(typeof(GameCell))]
    [CanEditMultipleObjects]
    public class GameCellEditor : Editor
    {
        private GameCell _gameCell;

        protected void OnEnable()
        {
            _gameCell = (GameCell) target;
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUI.changed)
            {
                _gameCell.Colorize(_gameCell.CellColor);
                EditorUtility.SetDirty(_gameCell);
                EditorSceneManager.MarkSceneDirty(_gameCell.gameObject.scene);
            }
        }
    }
}
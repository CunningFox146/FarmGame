using Farm.Interactable.GrowSystem;
using Farm.InventorySystem;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

namespace Farm.EditorTools
{
    [CustomEditor(typeof(Growable), true)]
    public class GrowableEditor : Editor
    {
        private Growable _target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _target = (Growable)target;
            Label($"Growth progress:", $"{_target.Progress / _target.StageGrowthTime * 100f}%");
            Label($"Stage:", $"{_target.CurrentStage} / {_target.StagesCount}");
        }

        private void Label(string key, string value)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(key);
            GUILayout.Label(value);
            EditorGUILayout.EndHorizontal();
        }
    }
}

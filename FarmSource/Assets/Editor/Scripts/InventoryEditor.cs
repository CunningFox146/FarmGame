using Farm.InventorySystem;
using UnityEditor;
using UnityEngine;

namespace Farm.EditorTools
{
    [CustomEditor(typeof(Inventory))]
    public class InventoryEditor : Editor
    {
        private Inventory _inventory;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _inventory = (Inventory)target;
            
            if (!Application.isPlaying) return;

            for (int i = 0; i < _inventory.MaxSize; i++)
            {
                RenderItemInfo(i);
            }
        }

        private void RenderItemInfo(int i)
        {
            var item = _inventory.Items[i];

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label($"Slot #{i}");
            GUILayout.Label(item ? item.name : "null");

            GUI.enabled = item is not null;
            if (GUILayout.Button("Drop"))
            {
                _inventory.Drop(i);
            }
            GUI.enabled = true;

            EditorGUILayout.EndHorizontal();
        }
    }
}

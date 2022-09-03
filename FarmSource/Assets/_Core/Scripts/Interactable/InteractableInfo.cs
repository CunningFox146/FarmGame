using UnityEngine;

namespace Farm.Interactable
{
    [CreateAssetMenu(menuName = "Interactable Info / Generic", order = -999)]
    public class InteractableInfo : ScriptableObject
    {
        [field: SerializeField] public int Priority { get; protected set; }
        [field: SerializeField] public float Distance { get; protected set; } = 1f;
    }
}

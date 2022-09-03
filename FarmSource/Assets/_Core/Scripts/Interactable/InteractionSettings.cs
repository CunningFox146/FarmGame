using UnityEngine;

namespace Farm.Interactable
{
    [CreateAssetMenu(menuName = "Interaction Settings / Generic", order = -999)]
    public class InteractionSettings : ScriptableObject
    {
        [field: SerializeField] public int Priority { get; protected set; }
        [field: SerializeField] public float Distance { get; protected set; } = 1f;
    }
}

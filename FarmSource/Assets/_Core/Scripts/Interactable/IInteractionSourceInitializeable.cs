using UnityEngine;

namespace Farm.Interactable
{
    public interface IInteractionSourceInitializeable<T> where T : Component
    {
        public void Init(T target);
    }
}

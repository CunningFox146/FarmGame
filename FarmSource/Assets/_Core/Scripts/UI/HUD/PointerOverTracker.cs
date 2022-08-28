using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Farm.UI.HUD
{
    public class PointerOverTracker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action PointerEnter;
        public event Action PointerExit;

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExit?.Invoke();
        }
    }
}

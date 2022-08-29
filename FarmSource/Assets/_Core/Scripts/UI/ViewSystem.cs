using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Farm.UI
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public class ViewSystem : MonoBehaviour
    {
        public event Action<View> OnViewShown;

        private Canvas ViewsCanvas;
        private GraphicRaycaster _raycaster;
        public List<View> Views { get; private set; }

        public Camera UICamera => ViewsCanvas.worldCamera;

        private void Awake()
        {
            _raycaster = GetComponent<GraphicRaycaster>();
            ViewsCanvas = GetComponent<Canvas>();
            RegisterViews();
        }

        public bool IsPointerOnUI(Vector3 pos)
        {
            var eventData = new PointerEventData(null);
            eventData.position = pos;
            var results = new List<RaycastResult>();
            _raycaster.Raycast(eventData, results);
            return results.Count > 0;
        }

        public T GetView<T>() where T : View
        {
            var view = Views.Where(v => v is T).FirstOrDefault();
            OnViewShown?.Invoke(view);
            return view as T;
        }

        public T ShowView<T>() where T : View
        {
            return ShowView(GetView<T>()) as T;
        }

        public View ShowView(View view)
        {
            view.Show();
            return view;
        }

        public T HideView<T>() where T : View
        {
            return HideView(GetView<T>()) as T;
        }

        public View HideView(View view)
        {
            view.Hide();
            return view;
        }

        public void HideAllViews()
        {
            Views.ForEach((view) => HideView(view));
        }

        public bool IsViewVisible<T>() where T : View
        {
            var view = GetView<T>();
            return view is not null && view.GetIsActive();
        }
        private void RegisterViews()
        {
            var views = GetComponentsInChildren<View>();
            Views = new(views);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Farm.UI
{
    [RequireComponent(typeof(Canvas))]
    public class ViewSystem : MonoBehaviour
    {
        public event Action<View> OnViewShown;

        private Canvas ViewsCanvas;
        public List<View> Views { get; private set; }

        public Camera UICamera => ViewsCanvas.worldCamera;

        private void Awake()
        {
            ViewsCanvas = GetComponent<Canvas>();
            RegisterViews();
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

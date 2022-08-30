using UnityEngine;

namespace Farm.UI
{
    public abstract class View : MonoBehaviour
    {
        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Hide() => gameObject.SetActive(false);

        public virtual bool GetIsActive() => gameObject.activeSelf;
    }
}
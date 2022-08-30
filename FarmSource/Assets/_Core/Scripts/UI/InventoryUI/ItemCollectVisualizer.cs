using DG.Tweening;
using Farm.InventorySystem;
using Farm.Util;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Farm.UI.InventoryUI
{
    public class ItemCollectVisualizer
    {
        private ObjectPool<Image> _icons;
        private Camera _gameplayCamera;
        private RectTransform _canvas;
        private InventoryDisplay _inventory;

        public ItemCollectVisualizer(InventoryDisplay inventory, Camera gameplayCamera, RectTransform canvas)
        {
            _gameplayCamera = gameplayCamera;
            _canvas = canvas;
            _inventory = inventory;

            InitPool();
        }

        private void InitPool()
        {
            _icons = new ObjectPool<Image>(
                CreateImage,
                (image) => image.gameObject.SetActive(true),
                (image) => image.gameObject.SetActive(false),
                null,
                true,
                10
            );
        }

        private Image CreateImage()
        {
            var obj = new GameObject("Icon");
            obj.transform.SetParent(_canvas);
            return obj.AddComponent<Image>();
        }

        public async void Visualize(InventoryItem item, InventorySlot targetSlot)
        {
            var startPos = UserInterfaceUtil.WorldToCanvasPosition(_gameplayCamera, _canvas, item.transform);
            var icon = _icons.Get();
            var iconTransform = icon.transform as RectTransform;

            icon.sprite = item.Info.Icon;

            iconTransform.sizeDelta = ((RectTransform)targetSlot.ItemIcon.transform).sizeDelta;
            iconTransform.localScale = Vector3.one * 0.5f;
            iconTransform.anchoredPosition = startPos;

            targetSlot.ItemIcon.gameObject.SetActive(false);

            var anim = DOTween.Sequence()
                .Append(iconTransform.DOMove(targetSlot.transform.position, 0.3f))
                .Join(iconTransform.DOScale(1f, 0.25f))
                .SetEase(Ease.OutCirc);

            await anim.AsyncWaitForCompletion();

            targetSlot.ItemIcon.gameObject.SetActive(true);
            _icons.Release(icon);
        }
    }
}

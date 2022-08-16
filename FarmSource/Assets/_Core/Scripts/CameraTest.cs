using DG.Tweening;
using UnityEngine;

namespace Farm
{
    public class CameraTest : MonoBehaviour
    {
        private Tween _tween;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _tween?.Complete();
                _tween = null;

                _tween = transform.DORotate(Vector3.up * 30f, 0.5f).SetRelative(true);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                _tween?.Complete();
                _tween = null;

                _tween = transform.DORotate(Vector3.up * -30f, 0.5f).SetRelative(true);
            }
        }
    }
}

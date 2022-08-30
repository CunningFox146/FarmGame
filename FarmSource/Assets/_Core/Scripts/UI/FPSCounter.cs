using TMPro;
using UnityEngine;

namespace Farm.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class FPSCounter : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            _text.text = (Time.frameCount / Time.time).ToString();
        }
    }
}

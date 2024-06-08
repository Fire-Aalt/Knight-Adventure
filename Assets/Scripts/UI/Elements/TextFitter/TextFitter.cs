using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [ExecuteInEditMode]
    public class TextFitter : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;
        [Title("Setings")]
        [SerializeField] private float _horizontalPadding;
        [SerializeField] private float _verticalPadding;
        [SerializeField, HideIf("usedByController")] private Vector2 _minSize;

        public Image Image => _image;
        public Vector2 PreferredImageSize { get; private set; }
        public Vector2 PreferredTextSize { get; private set; }

        public event Action<Vector2> OnPreferredSizeChanged;
        [HideInInspector] public bool usedByController = false;

        private Vector2 _cashedImageSize;
        private Vector2 _cashedTextSize;

        [Button, HideIf("usedByController")]
        public void SetCurrentSizeAsMin() => _minSize = _image.rectTransform.sizeDelta;

        [ContextMenu("Reset Used By Controller")]
        public void ResetUsedByController() => usedByController = false;


        [Button]
        public void ForceAdjust()
        {
            AdjustImage();
            _image.rectTransform.ForceUpdateRectTransforms();
            _cashedImageSize = _image.rectTransform.sizeDelta;
            _cashedTextSize = PreferredTextSize;
        }

        private void Update()
        {
            if (_image != null && _text != null)
            {
                PreferredTextSize = _text.GetPreferredValues();
                if (_image.rectTransform.sizeDelta != _cashedImageSize ||
                    PreferredTextSize != _cashedTextSize)
                {
                    ForceAdjust();
                }
            }
        }

        public void AdjustImage()
        {
            PreferredImageSize = GetTextSizeWithPadding();
            if (!usedByController)
            {
                _image.rectTransform.sizeDelta = PreferredImageSize;
            }
            OnPreferredSizeChanged?.Invoke(PreferredImageSize);
        }

        public Vector2 GetTextSizeWithPadding()
        {
            var preferredImageSize = new Vector2(PreferredTextSize.x + _horizontalPadding * 2, PreferredTextSize.y + _verticalPadding * 2);
            return new Vector2(Mathf.Max(_minSize.x, preferredImageSize.x), Mathf.Max(_minSize.y, preferredImageSize.y));
        }
    }
}

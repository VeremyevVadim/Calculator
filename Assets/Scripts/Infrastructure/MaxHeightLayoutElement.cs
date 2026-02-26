using UnityEngine;
using UnityEngine.UI;

namespace Calculator.Infrastructure
{
    public class MaxHeightLayoutElement : MonoBehaviour
    {
        [SerializeField]
        private float _maxHeight = 500f;

        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private LayoutElement _layoutElement;

        [SerializeField]
        private ScrollRect _scrollRect;

        [SerializeField]
        private RectTransform _parentRect;

        private bool _maxHeightReached;

        private void Update()
        {
            if (_content == null || _layoutElement == null)
            {
                return;
            }

            var preferredHeight = Mathf.Min(_content.rect.height, _maxHeight);
            _layoutElement.preferredHeight = preferredHeight;

            if (_content.rect.height >= _maxHeight && !_maxHeightReached)
            {
                _maxHeightReached = true;
                _scrollRect.vertical = true;
                _scrollRect.enabled = false;
                _scrollRect.enabled = true;
                Canvas.ForceUpdateCanvases();
            }
        }
    }
}
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

        private void Update()
        {
            if (_content == null || _layoutElement == null)
            {
                return;
            }

            var preferredHeight = Mathf.Min(_content.rect.height, _maxHeight);
            _layoutElement.preferredHeight = preferredHeight;

            var isNeedToEnableScrolling = _content.rect.height >= _maxHeight;
            _scrollRect.vertical = isNeedToEnableScrolling;
        }
    }
}
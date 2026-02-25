using System;
using UnityEngine;
using UnityEngine.UI;

namespace Calculator.Infrastructure
{
    public class ErrorPopupView : MonoBehaviour
    {
        [SerializeField]
        private Button _closeButton;

        public event Action CloseButtonClicked;

        private void Start()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        private void OnCloseButtonClicked()
        {
            CloseButtonClicked?.Invoke();
        }
    }
}

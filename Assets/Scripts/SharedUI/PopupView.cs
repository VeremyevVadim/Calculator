using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SharedUI
{
    public class PopupView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _messageText;

        [SerializeField]
        private TMP_Text _buttonText;

        [SerializeField]
        private Button _actionButton;

        private Action _onButtonClicked;

        private void Awake()
        {
            _actionButton.onClick.AddListener(HandleClick);
        }

        public void Show(string message, string buttonText, Action onClickAction = null)
        {
            _messageText.text = message;
            _buttonText.text = buttonText;
            _onButtonClicked = onClickAction;

            gameObject.SetActive(true);
            transform.SetAsLastSibling();
        }

        private void HandleClick()
        {
            gameObject.SetActive(false);
            _onButtonClicked?.Invoke();
        }

        private void OnDestroy()
        {
            _actionButton.onClick.RemoveListener(HandleClick);
        }
    }
}

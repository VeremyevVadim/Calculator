using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Calculator.Infrastructure
{
    public class InputFieldView : MonoBehaviour
    {
        [SerializeField]
        private Image _bottomLine;

        [SerializeField]
        private TMP_InputField _inputField;

        public event Action<string> ValueChanged;

        public string Text => _inputField.text;

        private void Start()
        {
            _inputField.onSelect.AddListener(OnInputFieldSelect);
            _inputField.onDeselect.AddListener(OnInputFieldDeselect);
            _inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
        }

        private void OnDestroy()
        {
            _inputField.onSelect.RemoveListener(OnInputFieldSelect);
            _inputField.onDeselect.RemoveListener(OnInputFieldDeselect);
            _inputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
        }

        public void SetText(string text)
        {
            _inputField.text = text;
        }


        private void OnInputFieldSelect(string _)
        {
            _bottomLine.enabled = true;
        }

        private void OnInputFieldDeselect(string _)
        {
            _bottomLine.enabled = false;
        }

        private void OnInputFieldValueChanged(string text)
        {
            ValueChanged?.Invoke(text);
        }
    }
}

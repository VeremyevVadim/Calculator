using System;
using TMPro;
using UnityEngine;

namespace Calculator.Infrastructure
{
    public class InputFieldView : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _inputField;

        public event Action<string> ValueChanged;

        public string Text => _inputField.text;
            
        private void Awake()
        {
            _inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
            _inputField.onValueChanged.AddListener(ClearInput);
        }

        private void OnDestroy()
        {
            _inputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
            _inputField.onValueChanged.RemoveListener(ClearInput);

        }

        public void SetText(string text)
        {
            _inputField.text = text;
        }

        private void ClearInput(string input)
        {
            if (input.Contains("\n") || input.Contains("\r") || input.Contains("\t") || input.Contains("\v"))
            {
                var clearedInput = input.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("\v", "");
                _inputField.text = clearedInput;
                _inputField.caretPosition = _inputField.text.Length;
            }
        }

        private void OnInputFieldValueChanged(string text)
        {
            ValueChanged?.Invoke(text);
        }
    }
}

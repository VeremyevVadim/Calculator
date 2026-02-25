using Calculator.Presentation;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Calculator.Infrastructure
{
    public class CalculatorWindowView : MonoBehaviour, ICalculatorView
    {
        [SerializeField]
        private InputFieldView _inputField;
        
        [SerializeField]
        private Button _button;

        [SerializeField]
        private RectTransform _resultElementsContainer;

        [SerializeField]
        private CalculationResultScrollElementView _prefab;

        [SerializeField]
        private ErrorPopupView _errorPopupView;

        [SerializeField]
        private CanvasGroup _calculatorCanvasGroup;

        public string InputFieldText => _inputField.Text;

        public event Action<string> CalculateButtonPressed;
        public event Action<string> InputFieldValueChanged;

        private void Start()
        {
            _button.onClick.AddListener(OnCalculateButtonPressed);
            _inputField.ValueChanged += OnInputFieldValueChanged;
            _errorPopupView.CloseButtonClicked += OnCloseButtonClicked;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnCalculateButtonPressed);
            _inputField.ValueChanged -= OnInputFieldValueChanged;
            _errorPopupView.CloseButtonClicked -= OnCloseButtonClicked;
        }

        public void Initialize(IEnumerable<string> results, string inputFieldText)
        {
            _inputField.SetText(inputFieldText);
            DisplayHistoryAsync(results).Forget();
        }

        public void AddResult(string result)
        {
            CreateCalculationResultElement(result);
        }

        public void ClearInputField()
        {
            _inputField.SetText(string.Empty);
        }

        public void ShowErrorPopup()
        {
            _errorPopupView.gameObject.SetActive(true);
        }

        private void OnCloseButtonClicked()
        {
            _errorPopupView.gameObject.SetActive(false);
        }

        private void CreateCalculationResultElement(string resultText)
        {
            var resultElement = Instantiate(_prefab, parent: _resultElementsContainer);
            resultElement.SetText(resultText);
        }

        private void OnInputFieldValueChanged(string text)
        {
            InputFieldValueChanged?.Invoke(text);
        }

        private void OnCalculateButtonPressed()
        {
            CalculateButtonPressed?.Invoke(_inputField.Text);
        }

        private async UniTaskVoid DisplayHistoryAsync(IEnumerable<string> history)
        {
            _calculatorCanvasGroup.alpha = 0f;

            foreach (var result in history)
            {
                var item = Instantiate(_prefab, parent: _resultElementsContainer);
                item.SetText(result);
            }

            Canvas.ForceUpdateCanvases();
            
            await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);

            _calculatorCanvasGroup.alpha = 1f;
        }
    }
}

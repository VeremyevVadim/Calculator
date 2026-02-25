using Calculator.Presentation;
using Cysharp.Threading.Tasks;
using SharedUI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

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
        private Transform _popupContainer;

        [SerializeField]
        private CanvasGroup _calculatorCanvasGroup;

        [SerializeField]
        private GameObject _historyBottomPaddingSpacer;

        public string InputFieldText => _inputField.Text;

        public event Action<string> CalculateButtonPressed;
        public event Action<string> InputFieldValueChanged;

        private CalculatorSettings _calculatorSettings;
        private PopupView _popupView;
        private bool _isSpacerEnabled;

        [Inject]
        public void Construct(CalculatorSettings settings)
        {
            _calculatorSettings = settings;
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnCalculateButtonPressed);
            _inputField.ValueChanged += OnInputFieldValueChanged;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnCalculateButtonPressed);
            _inputField.ValueChanged -= OnInputFieldValueChanged;
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
            if (_popupView == null)
            {
                _popupView = Instantiate(_calculatorSettings.PopupViewPrefab, _popupContainer);
            }

            _popupView.Show(message: _calculatorSettings.InvalidInputPopupMessage, buttonText: _calculatorSettings.InvalidInputPopupButton);
        }

        private void CreateCalculationResultElement(string resultText)
        {
            var resultElement = Instantiate(_calculatorSettings.ScrollElementPrefab, parent: _resultElementsContainer);
            resultElement.SetText(resultText);

            if (!_isSpacerEnabled)
            {
                _historyBottomPaddingSpacer.SetActive(true);
                _isSpacerEnabled = true;
            }
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
                CreateCalculationResultElement(result);
            }

            Canvas.ForceUpdateCanvases();
            
            await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);

            _calculatorCanvasGroup.alpha = 1f;
        }
    }
}

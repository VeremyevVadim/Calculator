using System;
using System.Collections.Generic;

namespace Calculator.Presentation
{
    public interface ICalculatorView
    {
        public event Action<string> CalculateButtonPressed;
        public event Action<string> InputFieldValueChanged;

        public string InputFieldText { get; }

        public void Initialize(IEnumerable<string> results, string inputFieldText);
        public void AddResult(string result);

        public void ClearInputField();
        public void ShowErrorPopup();
    }
}

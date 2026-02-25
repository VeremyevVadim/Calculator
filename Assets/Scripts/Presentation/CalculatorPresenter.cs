using Calculator.Domain;
using System;
using VContainer.Unity;

namespace Calculator.Presentation
{
    public class CalculatorPresenter : IStartable, IDisposable
    {
        private readonly ICalculatorView _view;
        private readonly PerformCalculationModel _calculationModel;

        public CalculatorPresenter(ICalculatorView view, PerformCalculationModel calculationModel)
        {
            _view = view;
            _calculationModel = calculationModel;
        }

        public void Start()
        {
            var saveData = _calculationModel.LoadHistory();
            _view.Initialize(saveData.History, saveData.CurrentInput);

            _view.CalculateButtonPressed += OnCalculateButtonPressed;
            _view.InputFieldValueChanged += OnCalculatorInputFieldValueChanged;
            _calculationModel.CalculationCompleted += OnCalculationCompleted;
        }

        public void Dispose()
        {
            _view.CalculateButtonPressed -= OnCalculateButtonPressed;
            _view.InputFieldValueChanged -= OnCalculatorInputFieldValueChanged;
            _calculationModel.CalculationCompleted -= OnCalculationCompleted;

            _calculationModel.SaveImmediately(_view.InputFieldText);
        }

        private void OnCalculateButtonPressed(string equation)
        {
            _calculationModel.Calculate(equation);
        }

        private void OnCalculatorInputFieldValueChanged(string equation)
        {
            _calculationModel.SaveDraft(equation).Forget();
        }

        private void OnCalculationCompleted(CalculationResult result)
        {
            _view.AddResult(result.Result);

            if (!result.IsValid)
            {
                _view.ShowErrorPopup();
            }
            else
            {
                _view.ClearInputField();
            }
        }
    }
}

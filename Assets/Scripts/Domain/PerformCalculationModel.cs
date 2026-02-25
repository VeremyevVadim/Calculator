using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Calculator.Domain
{
    public class PerformCalculationModel : IDisposable
    {
        public event Action<CalculationResult> CalculationCompleted;

        private readonly IEnumerable<ICalculatorOperation> _operations;
        private readonly ICalculatorRepository _repository;
        private readonly IEquationValidator _validator;
        private readonly ICalculatorSettings _settings;


        private CancellationTokenSource _saveCts;
        private List<string> _calculatorHistory = new(20);

        public PerformCalculationModel(
            IEnumerable<ICalculatorOperation> operations,
            ICalculatorRepository repository,
            IEquationValidator validator,
            ICalculatorSettings settings)
        {
            _operations = operations;
            _repository = repository;
            _validator = validator;
            _settings = settings;
        }

        public SaveData LoadHistory()
        {
            var saveData = _repository.Load();

            _calculatorHistory = saveData.History ?? new List<string>();

            return saveData;
        }

        public void Calculate(string equation)
        {
            var isEquationValid = _validator.Validate(equation);

            if (!isEquationValid)
            {
                CompleteCalculation(equation, $"{equation}={_settings.ErrorResultText}");
                return;
            }

            var operation = _operations.FirstOrDefault(o => equation.Contains(o.Symbol));
            if (operation == null)
            {
                return;
            }

            var parts = equation.Split(new[] { operation.Symbol }, StringSplitOptions.RemoveEmptyEntries);
            var result = parts[0];

            for (var i = 1; i < parts.Length; i++)
            {
                result = operation.Execute(result, parts[i]);
            }

            CompleteCalculation(equation, $"{equation}={result}", isEquationValid);
        }

        public async UniTaskVoid SaveDraft(string currentInput)
        {
            _saveCts?.Cancel();
            _saveCts?.Dispose();
            _saveCts = new CancellationTokenSource();

            try
            {
                await UniTask.Delay(TimeSpan.FromMilliseconds(_settings.InputDebounceTimeMs), cancellationToken: _saveCts.Token);

                _repository.Save(currentInput, _calculatorHistory);
            }
            catch (OperationCanceledException)
            {
            }
        }

        public void SaveImmediately(string currentInput)
        {
            _saveCts?.Cancel();
            _repository.Save(currentInput, _calculatorHistory);
        }

        public void Dispose()
        {
            _saveCts?.Cancel();
            _saveCts?.Dispose(); 
        }

        private void CompleteCalculation(string equation, string historyEntry, bool isEquationValid = false)
        {
            CalculationResult calculationResult = new CalculationResult(isEquationValid, historyEntry);
            _calculatorHistory.Add(calculationResult.Result);
            SaveImmediately(equation);
            CalculationCompleted?.Invoke(calculationResult);
        }
    }
}

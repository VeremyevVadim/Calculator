using Calculator.Domain;
using Calculator.Infrastructure;
using Calculator.Presentation;
using NUnit.Framework;
using System.Collections.Generic;

public class PreformCalculationModelTests
{
    private PerformCalculationModel _model;
    private List<ICalculatorOperation> _operations;
    private ICalculatorSettings _calculatorSettings;

    [SetUp]
    public void SetUp()
    {
        _operations = new List<ICalculatorOperation> { new AdditionOperation() };
        _calculatorSettings = new CalculatorSettings();
        _model = new PerformCalculationModel(
            _operations,
            new MockRepository(),
            new EquationValidator(),
            _calculatorSettings
        );
    }

    [Test]
    public void Calculate_SmallNumbers_ReturnsCorrectString()
    {
        var input = "100+200";
        var expectedResult = $"100+200=300";

        CalculationResult capturedResult = null;
        _model.CalculationCompleted += (res) => capturedResult = res;

        _model.Calculate(input);

        Assert.IsTrue(capturedResult.IsValid);
        Assert.AreEqual(expectedResult, capturedResult.Result);
    }

    [Test]
    public void Calculate_MultipleNumbers_ReturnsCorrectString()
    {
        var input = "100+200+300";
        var expectedResult = "100+200+300=600";

        CalculationResult capturedResult = null;
        _model.CalculationCompleted += (res) => capturedResult = res;

        _model.Calculate(input);

        Assert.IsTrue(capturedResult.IsValid);
        Assert.AreEqual(expectedResult, capturedResult.Result);
    }

    [Test]
    public void Calculate_HugeNumbers_ReturnsCorrectString()
    {
        var input = "1111111111111111111111111111111111111111111111111111111+1111111111111111111111111111111111111111111111111111111";
        var expectedResult = "1111111111111111111111111111111111111111111111111111111+1111111111111111111111111111111111111111111111111111111=2222222222222222222222222222222222222222222222222222222";

        CalculationResult capturedResult = null;
        _model.CalculationCompleted += (res) => capturedResult = res;

        _model.Calculate(input);

        Assert.IsTrue(capturedResult.IsValid);
        Assert.AreEqual(expectedResult, capturedResult.Result);
    }

    [Test]
    public void Calculate_InvalidInput_ReturnsErrorEntry()
    {
        var input = "2+q";
        var expectedResult = $"2+q={_calculatorSettings.ErrorResultText}";
        CalculationResult capturedResult = null;
        _model.CalculationCompleted += (res) => capturedResult = res;

        _model.Calculate(input);

        Assert.IsFalse(capturedResult.IsValid);
        Assert.AreEqual(expectedResult, capturedResult.Result);

    }

    private class MockRepository : ICalculatorRepository
    {
        public void Save(string input, List<string> history) { }
        public SaveData Load() => new SaveData { History = new List<string>() };
    }
}

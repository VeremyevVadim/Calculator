namespace Calculator.Domain
{
    public interface ICalculatorSettings
    {
        public string ErrorResultText { get; }
        public int InputDebounceTimeMs { get; }
    }
}

namespace Calculator.Domain
{
    public class CalculationResult
    {
        public bool IsValid { get; private set; }
        public string Result { get; private set; }

        public CalculationResult(bool isValid, string result)
        {
            IsValid = isValid;
            Result = result;
        }
    }
}

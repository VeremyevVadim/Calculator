namespace Calculator.Domain
{
    public interface ICalculatorOperation
    {
        string Symbol { get; }
        string Execute(string a, string b);
    }
}

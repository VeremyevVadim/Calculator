using System.Numerics;

namespace Calculator.Domain
{
    public class AdditionOperation : ICalculatorOperation
    {
        public string Symbol => "+";
        public string Execute(string a, string b) => SumLargeNumbers(a, b);

        private string SumLargeNumbers(string a, string b)
        {
            return (BigInteger.Parse(a) + BigInteger.Parse(b)).ToString();
        }
    }
}

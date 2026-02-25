using Calculator.Domain;
using System.Text.RegularExpressions;

namespace Calculator.Infrastructure
{
    public class EquationValidator : IEquationValidator
    {
        private readonly Regex ExpressionRegex = new Regex(@"^\d+(\+\d+)+$", RegexOptions.Compiled);

        public bool Validate(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            return ExpressionRegex.IsMatch(input);
        }
    }
}
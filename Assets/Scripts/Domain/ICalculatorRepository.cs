using System.Collections.Generic;

namespace Calculator.Domain
{
    public interface ICalculatorRepository
    {
        public void Save(string currentInput, List<string> history);
        public SaveData Load();
    }
}

using System.Collections.Generic;

namespace Calculator.Domain
{
    [System.Serializable]
    public class SaveData 
    {
        public string CurrentInput;
        public List<string> History;
    }
}

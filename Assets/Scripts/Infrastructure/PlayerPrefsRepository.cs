using Calculator.Domain;
using System.Collections.Generic;
using UnityEngine;

namespace Calculator.Infrastructure
{
    public class PlayerPrefsRepository : ICalculatorRepository
    {
        private const string SaveKey = "CalculatorData";

        public void Save(string currentInput, List<string> history)
        {
            var data = new SaveData { CurrentInput = currentInput, History = history };
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        public SaveData Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey))
            {
                return new SaveData { CurrentInput = string.Empty, History = new List<string>() };
            }

            var json = PlayerPrefs.GetString(SaveKey);
            var data = JsonUtility.FromJson<SaveData>(json);
            return new SaveData { CurrentInput = data.CurrentInput, History = data.History };
        }
    }
}

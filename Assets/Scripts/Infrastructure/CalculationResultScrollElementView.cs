using TMPro;
using UnityEngine;

namespace Calculator.Infrastructure
{
    public class CalculationResultScrollElementView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _resultText;

        public void SetText(string text)
        {
            _resultText.SetText(text);
        }
    }
}

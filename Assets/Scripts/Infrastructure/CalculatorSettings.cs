using Calculator.Domain;
using Calculator.Infrastructure;
using SharedUI;
using UnityEngine;

namespace Calculator.Presentation
{
    [CreateAssetMenu(fileName = "CalculatorSettings", menuName = "Calculator/Settings")]
    public class CalculatorSettings : ScriptableObject, ICalculatorSettings
    {
        [SerializeField]
        private int DebounceTime = 500;

        [SerializeField]
        private string ErrorText = "Error";

        [Header("Prefabs")]
        public CalculationResultScrollElementView ScrollElementPrefab;
        public PopupView PopupViewPrefab;

        [Header("Localization/Texts")]
        public string InvalidInputPopupMessage = "Please check the expression you just entered";
        public string InvalidInputPopupButton = "Got it";

        public int InputDebounceTimeMs => DebounceTime;
        public string ErrorResultText => ErrorText;
    }
}

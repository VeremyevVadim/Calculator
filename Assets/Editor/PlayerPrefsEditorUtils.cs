using UnityEditor;
using UnityEngine;

public static class PlayerPrefsEditorUtils
{
    [MenuItem("Tools/Calculator/Clear All PlayerPrefs")]
    public static void ClearAllPrefs()
    {
        if (EditorUtility.DisplayDialog("Clear PlayerPrefs",
                "Вы уверены, что хотите удалить ВСЕ данные PlayerPrefs? " +
                "Это действие нельзя отменить.", "Да, удалить", "Отмена"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("<color=green>[Storage]</color> PlayerPrefs полностью очищены.");
        }
    }

    [MenuItem("Tools/Calculator/Clear Only Calculator Data")]
    public static void ClearCalculatorData()
    {
        const string saveKey = "CalculatorData";
        if (PlayerPrefs.HasKey(saveKey))
        {
            PlayerPrefs.DeleteKey(saveKey);
            PlayerPrefs.Save();
            Debug.Log($"<color=green>[Storage]</color> Данные по ключу '{saveKey}' удалены.");
        }
        else
        {
            Debug.Log("<color=yellow>[Storage]</color> Сохраненных данных калькулятора не найдено.");
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QualityDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;

    void Start()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        _dropdown.ClearOptions();

        string[] qualityLevels = QualitySettings.names;

        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (string level in qualityLevels) options.Add(new TMP_Dropdown.OptionData(level));

        _dropdown.AddOptions(options);
        _dropdown.value = QualitySettings.GetQualityLevel();

        _dropdown.onValueChanged.AddListener(delegate
        {
            QualitySettings.SetQualityLevel(_dropdown.value, true);
        });
    }
}
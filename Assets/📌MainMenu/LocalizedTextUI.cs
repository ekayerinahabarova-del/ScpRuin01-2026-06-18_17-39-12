using UnityEngine;
using TMPro;

public class LocalizedTextUI : MonoBehaviour
{
    public string localizationKey;
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    public void UpdateText()
    {
        if (textMesh != null)
        {
            textMesh.text = LocalizationManager.Instance.GetText(localizationKey);
        }
    }
}
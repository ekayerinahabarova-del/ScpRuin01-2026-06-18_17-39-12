using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsPanel : MonoBehaviour
{
    [Header("Язык")]
    public TMP_Dropdown languageDropdown;

    [Header("Графика")]
    public TMP_Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    public Slider brightnessSlider;

    [Header("Звук")]
    public Slider masterVolumeSlider;

    [Header("FPS")]
    public TMP_Dropdown fpsDropdown;
    public Toggle showFpsToggle;

    [Header("Подсказки")]
    public Toggle tipsToggle;
    public Toggle subtitlesToggle;

    void Start()
    {
        LoadSettings();
        SetupListeners();

        // Подсказки по умолчанию ВЫКЛ
        if (tipsToggle != null)
            tipsToggle.isOn = false;

        // Субтитры по умолчанию ВКЛ
        if (subtitlesToggle != null)
            subtitlesToggle.isOn = true;
    }

    void SetupListeners()
    {
        if (languageDropdown != null)
            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);

        if (qualityDropdown != null)
            qualityDropdown.onValueChanged.AddListener(SetQuality);

        if (fullscreenToggle != null)
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        if (brightnessSlider != null)
            brightnessSlider.onValueChanged.AddListener(SetBrightness);

        if (masterVolumeSlider != null)
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);

        if (showFpsToggle != null)
            showFpsToggle.onValueChanged.AddListener(ToggleFpsDisplay);
    }

    void OnLanguageChanged(int index)
    {
        LocalizationManager.Instance.SetLanguage((Language)index);
        PlayerPrefs.SetInt("Language", index);
        PlayerPrefs.Save();
    }

    void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("Quality", index);
    }

    void SetFullscreen(bool isFull)
    {
        Screen.fullScreen = isFull;
        PlayerPrefs.SetInt("Fullscreen", isFull ? 1 : 0);
    }

    void SetBrightness(float value)
    {
        RenderSettings.ambientIntensity = value;
        PlayerPrefs.SetFloat("Brightness", value);
    }

    void SetMasterVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    void ToggleFpsDisplay(bool show)
    {
        PlayerPrefs.SetInt("ShowFPS", show ? 1 : 0);
    }

    void LoadSettings()
    {
        if (languageDropdown != null)
            languageDropdown.value = PlayerPrefs.GetInt("Language", 0);

        if (qualityDropdown != null)
            qualityDropdown.value = PlayerPrefs.GetInt("Quality", 2);

        if (fullscreenToggle != null)
            fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;

        if (brightnessSlider != null)
            brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 1f);

        if (masterVolumeSlider != null)
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);

        if (showFpsToggle != null)
            showFpsToggle.isOn = PlayerPrefs.GetInt("ShowFPS", 0) == 1;
    }
}
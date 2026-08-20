using UnityEngine;
using System.Collections.Generic;

public enum Language
{
    Russian,
    English,
    Ukrainian,
    Belarusian,
    Kazakh,
    German
}

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    [System.Serializable]
    public class LocalizedText
    {
        public string key;
        public string russian;
        public string english;
        public string ukrainian;
        public string belarusian;
        public string kazakh;
        public string german;
    }

    public List<LocalizedText> texts;
    public Language currentLanguage = Language.Russian;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadLanguage();
    }

    public string GetText(string key)
    {
        foreach (LocalizedText text in texts)
        {
            if (text.key == key)
            {
                switch (currentLanguage)
                {
                    case Language.Russian: return text.russian;
                    case Language.English: return text.english;
                    case Language.Ukrainian: return text.ukrainian;
                    case Language.Belarusian: return text.belarusian;
                    case Language.Kazakh: return text.kazakh;
                    case Language.German: return text.german;
                }
            }
        }
        return key;
    }

    public void SetLanguage(Language language)
    {
        currentLanguage = language;
        PlayerPrefs.SetInt("Language", (int)language);
        PlayerPrefs.Save();
    }

    void LoadLanguage()
    {
        int savedLang = PlayerPrefs.GetInt("Language", 0);
        currentLanguage = (Language)savedLang;
    }
}
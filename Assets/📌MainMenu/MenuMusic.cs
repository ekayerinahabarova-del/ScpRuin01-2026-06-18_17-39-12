using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [Header("Настройки")]
    public AudioSource musicSource;
    public float fadeInDuration = 2f;    // Плавное появление
    public float fadeOutDuration = 1f;   // Плавное затухание

    private static MenuMusic instance;
    private float originalVolume;

    void Awake()
    {
        // Чтобы музыка не дублировалась при перезагрузке сцены
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (musicSource == null)
            musicSource = GetComponent<AudioSource>();

        originalVolume = musicSource.volume;
        musicSource.volume = 0;
        musicSource.Play();

        // Плавное появление
        StartCoroutine(FadeIn());
    }

    System.Collections.IEnumerator FadeIn()
    {
        float elapsed = 0;
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0, originalVolume, elapsed / fadeInDuration);
            yield return null;
        }
        musicSource.volume = originalVolume;
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutAndStop());
    }

    System.Collections.IEnumerator FadeOutAndStop()
    {
        float elapsed = 0;
        float startVol = musicSource.volume;

        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVol, 0, elapsed / fadeOutDuration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVol;
    }
}
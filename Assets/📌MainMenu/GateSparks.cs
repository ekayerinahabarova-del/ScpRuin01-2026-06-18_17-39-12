using UnityEngine;
using System.Collections;

public class GateSparks : MonoBehaviour
{
    [Header("Ссылки")]
    public ParticleSystem sparks;        // Система частиц для искр
    public AudioSource audioSource;      // Источник звука
    public AudioClip sparkSound;         // Звук искр
    public Light flickerLight;           // Свет для эффекта мерцания (опционально)

    [Header("Настройки")]
    public float interval = 3f;          // Интервал между искрами (3 секунды)
    public float sparkDuration = 0.5f;   // Длительность эффекта
    public float lightIntensity = 2f;    // Интенсивность света при искре

    private float originalLightIntensity;

    void Start()
    {
        // Если не назначен источник звука, добавляем его
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Сохраняем оригинальную интенсивность света
        if (flickerLight != null)
            originalLightIntensity = flickerLight.intensity;

        // Запускаем цикл искр
        StartCoroutine(SparksRoutine());
    }

    IEnumerator SparksRoutine()
    {
        while (true)
        {
            // Ждём 3 секунды
            yield return new WaitForSeconds(interval);

            // Воспроизводим эффект искр
            StartCoroutine(SparksEffect());
        }
    }

    IEnumerator SparksEffect()
    {
        // Включаем искры
        if (sparks != null)
        {
            sparks.Play();
            Debug.Log("Искры! ⚡");
        }

        // Звук искр
        if (audioSource != null && sparkSound != null)
            audioSource.PlayOneShot(sparkSound);

        // Эффект мерцания света
        if (flickerLight != null)
        {
            float elapsed = 0f;
            while (elapsed < sparkDuration)
            {
                elapsed += Time.deltaTime;
                // Случайное мерцание
                flickerLight.intensity = originalLightIntensity + Random.Range(0, lightIntensity);
                yield return null;
            }
            flickerLight.intensity = originalLightIntensity;
        }
        else
        {
            yield return new WaitForSeconds(sparkDuration);
        }

        // Выключаем искры
        if (sparks != null)
            sparks.Stop();
    }
}
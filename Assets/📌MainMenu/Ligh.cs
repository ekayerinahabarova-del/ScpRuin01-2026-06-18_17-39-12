using UnityEngine;
using System.Collections;

public class ScpLightController : MonoBehaviour
{
    [Header("Ссылки")]
    public Light targetLight;
    public Animator scpAnimator;

    [Header("Яркость (УВЕЛИЧЕНА ВДВОЕ)")]
    [Range(1, 100)] public float fullIntensity = 100f;    // Было 50, теперь 100
    public Color lightColor = new Color(1f, 0.95f, 0.9f);

    [Header("Мерцание")]
    public bool enableFlicker = true;
    public float flickerMinInterval = 0.5f;
    public float flickerMaxInterval = 2f;
    public float flickerDuration = 0.05f;

    [Header("Цикличное отключение")]
    public float onDuration = 29f;        // Горит 29 секунд
    public float offDuration = 3f;        // Выключен 3 секунды

    private float timer = 0f;
    private bool isOn = true;
    private float originalIntensity;

    void Start()
    {
        if (targetLight == null)
            targetLight = GetComponent<Light>();

        originalIntensity = fullIntensity;

        // Максимальные настройки света
        targetLight.intensity = fullIntensity;
        targetLight.color = lightColor;
        targetLight.enabled = true;
        targetLight.shadowStrength = 1f;
        targetLight.useColorTemperature = false;

        // Дополнительное увеличение яркости через рендеринг
        targetLight.intensity = fullIntensity;

        // Запускаем цикл отключения
        StartCoroutine(LightCycleRoutine());

        // Запускаем мерцание
        if (enableFlicker)
            StartCoroutine(FlickerRoutine());
    }

    IEnumerator LightCycleRoutine()
    {
        while (true)
        {
            // Свет включён 29 секунд
            isOn = true;
            targetLight.enabled = true;
            targetLight.intensity = originalIntensity;

            yield return new WaitForSeconds(onDuration);

            // Предупреждающие мерцания перед выключением
            for (int i = 0; i < 3; i++)
            {
                targetLight.intensity = originalIntensity * 0.3f;
                yield return new WaitForSeconds(0.05f);
                targetLight.intensity = originalIntensity;
                yield return new WaitForSeconds(0.05f);
            }

            // Выключаем свет на 3 секунды
            isOn = false;
            targetLight.enabled = false;

            yield return new WaitForSeconds(offDuration);
        }
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(flickerMinInterval, flickerMaxInterval));

            // Мерцаем только если свет включён
            if (isOn)
            {
                targetLight.intensity = 0f;
                yield return new WaitForSeconds(flickerDuration);
                targetLight.intensity = originalIntensity;

                // Иногда двойное мерцание
                if (Random.value > 0.7f)
                {
                    yield return new WaitForSeconds(flickerDuration);
                    targetLight.intensity = 0f;
                    yield return new WaitForSeconds(flickerDuration);
                    targetLight.intensity = originalIntensity;
                }
            }
        }
    }
}
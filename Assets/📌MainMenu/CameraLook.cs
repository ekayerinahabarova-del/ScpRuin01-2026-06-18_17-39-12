using UnityEngine;

public class SmoothCameraWithReturn : MonoBehaviour
{
    [Header("Настройки")]
    public float sensitivity = 0.6f;        // Чувствительность (уменьшена в 2 раза от 1.2)
    public float smoothTime = 0.1f;
    public float returnDelay = 2f;
    public float returnSpeed = 2f;
    public float maxAngle = 12.5f;          // Макс. угол отклонения (уменьшен в 2 раза от 25)

    private Vector3 currentVelocity;
    private Vector3 targetDelta;
    private Vector3 currentDelta;
    private Quaternion originalRotation;
    private float lastMoveTime;
    private bool returning = false;

    void Start()
    {
        originalRotation = transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Ввод с мыши
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Проверяем движение
        if (Mathf.Abs(mouseX) > 0.01f || Mathf.Abs(mouseY) > 0.01f)
        {
            lastMoveTime = Time.time;
            returning = false;

            // Накопление движения (ограничиваем максимальным углом)
            targetDelta.x = Mathf.Clamp(targetDelta.x + mouseX, -maxAngle, maxAngle);
            targetDelta.y = Mathf.Clamp(targetDelta.y - mouseY, -maxAngle, maxAngle);
        }

        // Возврат через 2 секунды бездействия
        if (!returning && Time.time - lastMoveTime > returnDelay)
        {
            returning = true;
        }

        if (returning)
        {
            // Плавно возвращаем targetDelta к нулю (но не трогаем саму камеру принудительно)
            targetDelta = Vector3.Lerp(targetDelta, Vector3.zero, returnSpeed * Time.deltaTime);

            if (targetDelta.magnitude < 0.05f)
            {
                targetDelta = Vector3.zero;
                returning = false;
            }
        }

        // Плавное движение текущей позиции к цели
        currentDelta = Vector3.SmoothDamp(currentDelta, targetDelta, ref currentVelocity, smoothTime);

        // Применяем поворот только к currentDelta, без принудительного сброса камеры
        Quaternion targetRot = originalRotation * Quaternion.Euler(currentDelta.y, currentDelta.x, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, smoothTime * 10f * Time.deltaTime);
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }
}
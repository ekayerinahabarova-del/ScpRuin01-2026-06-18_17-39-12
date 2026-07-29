using UnityEngine;

public class HelicopterFlight : MonoBehaviour
{
    [Header("Движение вперёд")]
    public float forwardSpeed = 15f;
    public float acceleration = 5f;
    public float maxSpeed = 30f;

    [Header("Покачивания (Bobbing)")]
    public float bobSpeed = 0.8f;
    public float bobHeight = 0.4f;
    public float bobPhase = 0f;

    [Header("Наклоны (Tilting)")]
    public float tiltAmount = 5f;
    public float tiltSpeed = 0.5f;

    [Header("Вибрaция (для реализма)")]
    public float vibrationSpeed = 60f;
    public float vibrationAmount = 0.05f;

    [Header("Повороты (Yaw)")]
    public float yawSpeed = 10f;
    public float yawAmount = 3f;

    private Vector3 startPos;
    private float startTime;
    private float currentSpeed;

    void Start()
    {
        startPos = transform.position;
        startTime = Time.time;
        currentSpeed = forwardSpeed;
    }

    void Update()
    {
        // 1. Плавный разгон
        if (currentSpeed < maxSpeed)
            currentSpeed += acceleration * Time.deltaTime;

        // 2. Движение вперёд
        transform.position += transform.forward * currentSpeed * Time.deltaTime;

        // 3. Покачивание вверх-вниз (bobbing)
        float bob = Mathf.Sin((Time.time - startTime) * bobSpeed + bobPhase) * bobHeight;
        transform.position += Vector3.up * bob * Time.deltaTime;

        // 4. Наклоны (крен и тангаж)
        float tiltX = Mathf.Sin((Time.time - startTime) * tiltSpeed) * tiltAmount;
        float tiltZ = Mathf.Sin((Time.time - startTime) * tiltSpeed * 0.7f + 0.5f) * tiltAmount * 0.3f;
        transform.rotation = Quaternion.Euler(tiltX, transform.rotation.eulerAngles.y, tiltZ);

        // 5. Вибрация (мелкая дрожь)
        float vibX = Mathf.Sin((Time.time - startTime) * vibrationSpeed) * vibrationAmount;
        float vibY = Mathf.Sin((Time.time - startTime) * vibrationSpeed * 1.3f + 1f) * vibrationAmount;
        float vibZ = Mathf.Sin((Time.time - startTime) * vibrationSpeed * 0.7f + 2f) * vibrationAmount;
        transform.position += new Vector3(vibX, vibY, vibZ) * Time.deltaTime;

        // 6. Медленные повороты влево-вправо
        float yaw = Mathf.Sin((Time.time - startTime) * 0.2f) * yawAmount;
        transform.Rotate(0, yaw * Time.deltaTime * yawSpeed, 0);
    }
}
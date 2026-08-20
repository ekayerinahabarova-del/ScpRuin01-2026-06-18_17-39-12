using UnityEngine;

public class CameraLookFinal : MonoBehaviour
{
    [Header("Настройки")]
    public float sensitivity = 2f;
    public float smoothTime = 0.5f;              // Ещё плавнее
    public float maxHorizontalAngle = 46.5f;     // Уменьшен на 7% (50 - 7% = 46.5)
    public float maxVerticalAngle = 25.1f;       // Уменьшен на 7% (27 - 7% = 25.1)

    private float xRotation = 0f;
    private float yRotation = 0f;
    private float currentX = 0f;
    private float currentY = 0f;
    private float velX = 0f;
    private float velY = 0f;
    private Vector3 startRot;

    void Start()
    {
        startRot = transform.localEulerAngles;

        float sx = startRot.x;
        float sy = startRot.y;
        if (sx > 180) sx -= 360;
        if (sy > 180) sy -= 360;

        xRotation = sx;
        yRotation = sy;
        currentX = sx;
        currentY = sy;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        xRotation -= mouseY;
        yRotation += mouseX;

        float sx = startRot.x;
        if (sx > 180) sx -= 360;
        xRotation = Mathf.Clamp(xRotation, sx - maxVerticalAngle, sx + maxVerticalAngle);

        float sy = startRot.y;
        if (sy > 180) sy -= 360;
        yRotation = Mathf.Clamp(yRotation, sy - maxHorizontalAngle, sy + maxHorizontalAngle);

        currentX = Mathf.SmoothDamp(currentX, xRotation, ref velX, smoothTime * Time.deltaTime);
        currentY = Mathf.SmoothDamp(currentY, yRotation, ref velY, smoothTime * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(currentX, currentY, 0f);
    }
}
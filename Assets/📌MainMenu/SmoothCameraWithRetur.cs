using UnityEngine;

public class CameraControllerFinal : MonoBehaviour
{
    [Header("Настройки")]
    public float sensitivity = 0.3f;
    public float smoothTime = 0.25f;
    public float returnDelay = 3f;
    public float returnSpeed = 1f;
    public float maxAngle = 5f;

    private Vector3 currentVelocity;
    private Vector3 targetDelta;
    private Vector3 currentDelta;
    private Quaternion originalRotation;
    private float lastMoveTime;
    private bool returning = false;

    void Start()
    {
        originalRotation = transform.localRotation;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        if (Mathf.Abs(mouseX) > 0.01f || Mathf.Abs(mouseY) > 0.01f)
        {
            lastMoveTime = Time.time;
            returning = false;

            targetDelta.x = Mathf.Clamp(targetDelta.x + mouseX, -maxAngle, maxAngle);
            targetDelta.y = Mathf.Clamp(targetDelta.y - mouseY, -maxAngle, maxAngle);
        }

        if (!returning && Time.time - lastMoveTime > returnDelay)
        {
            returning = true;
        }

        if (returning)
        {
            targetDelta = Vector3.Lerp(targetDelta, Vector3.zero, returnSpeed * Time.deltaTime);

            if (targetDelta.magnitude < 0.05f)
            {
                targetDelta = Vector3.zero;
                currentDelta = Vector3.zero;
                returning = false;
            }
        }

        currentDelta = Vector3.SmoothDamp(currentDelta, targetDelta, ref currentVelocity, smoothTime);

        Quaternion targetRot = originalRotation * Quaternion.Euler(currentDelta.y, currentDelta.x, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, smoothTime * 8f * Time.deltaTime);
    }
}
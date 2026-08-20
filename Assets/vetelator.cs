using UnityEngine;

public class RotorSpin : MonoBehaviour
{
    public float speed = 500f;
    public Vector3 axis = Vector3.up;

    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void LateUpdate()
    {
        // Вращаем
        transform.Rotate(axis * speed * Time.deltaTime, Space.World);

        // Возвращаем позицию
        transform.position = startPos;
    }
}
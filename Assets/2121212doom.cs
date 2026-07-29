using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float speed = 2f;
    public float spawnDistance = 100f;
    public float heightVariation = 10f;

    private float startX;
    private float startY;

    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
    }

    void Update()
    {
        // Облака двигаются назад
        transform.position += Vector3.back * speed * Time.deltaTime;

        // Зацикливание
        if (transform.position.z < -spawnDistance)
        {
            transform.position = new Vector3(
                Random.Range(-spawnDistance, spawnDistance),
                startY + Random.Range(-heightVariation, heightVariation),
                spawnDistance
            );
        }
    }
}
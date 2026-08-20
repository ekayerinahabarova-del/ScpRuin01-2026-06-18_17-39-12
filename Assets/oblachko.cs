using UnityEngine;

public class CloudMoverNew : MonoBehaviour
{
    public float speed = 3f;
    public float spawnDistance = 100f;
    public float heightVariation = 15f;

    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;

        if (transform.position.z < -spawnDistance)
        {
            transform.position = new Vector3(
                Random.Range(-spawnDistance, spawnDistance),
                startY + Random.Range(-heightVariation, heightVariation),
                spawnDistance + Random.Range(0, 20)
            );
        }
    }
}
using UnityEngine;

public class OBLAKO : MonoBehaviour
{
    [Header("Движение")]
    public float speed = 3f;
    public float spawnDistance = 500f;          // Увеличено до 500
    public float heightVariation = 50f;

    [Header("Запретная зона")]
    public Vector3 forbiddenZoneCenter = new Vector3(0f, -0.42f, 0f);
    public float forbiddenRadius = 10f;

    [Header("Путь (коридор)")]
    public float pathMinX = -5f;
    public float pathMaxX = 5f;
    public float pathMinZ = -5f;
    public float pathMaxZ = 5f;

    [Header("Радиус переспавна (120 метров)")]
    public float respawnRadius = 120f;

    [Header("Зона спавна (ДАЛЕКО, невидимо)")]
    public float spawnMinX = 100f;              // Минимум 100 метров в сторону
    public float spawnMaxX = 250f;              // Максимум 250 метров в сторону
    public float spawnMinZ = 200f;              // Минимум 200 метров по Z (впереди или сзади)
    public float spawnMaxZ = 400f;              // Максимум 400 метров по Z

    private float startY;

    void Start()
    {
        startY = transform.position.y;

        if (IsInRespawnZone(transform.position))
        {
            transform.position = GetValidSpawnPosition();
        }
    }

    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;

        if (IsInRespawnZone(transform.position))
        {
            transform.position = GetValidSpawnPosition();
            return;
        }

        if (transform.position.z < -spawnDistance)
        {
            transform.position = GetValidSpawnPosition();
        }
    }

    bool IsInRespawnZone(Vector3 position)
    {
        float dist = Vector3.Distance(position, forbiddenZoneCenter);
        return dist < respawnRadius;
    }

    Vector3 GetValidSpawnPosition()
    {
        Vector3 newPos;
        int attempts = 0;
        bool valid = false;

        do
        {
            // Случайная сторона
            float side = Random.value > 0.5f ? 1f : -1f;

            // X: ДАЛЕКО в сторону (100-250 метров)
            float xPos = Random.Range(spawnMinX, spawnMaxX) * side;

            // Z: ДАЛЕКО (200-400 метров вперёд или назад)
            float zPos;
            if (Random.value > 0.5f)
            {
                zPos = Random.Range(spawnMinZ, spawnMaxZ);
            }
            else
            {
                zPos = -Random.Range(spawnMinZ, spawnMaxZ);
            }

            float yPos = startY + Random.Range(-heightVariation, heightVariation);

            newPos = new Vector3(xPos, yPos, zPos);

            attempts++;

            // Проверка: позиция НЕ должна быть в радиусе 120 метров
            bool inRespawnZone = IsInRespawnZone(newPos);

            // Проверка на старую запретную зону
            float distToForbidden = Vector3.Distance(newPos, forbiddenZoneCenter);
            bool inForbidden = distToForbidden < forbiddenRadius;

            // Проверка на путь
            bool inPath = (newPos.x >= pathMinX && newPos.x <= pathMaxX &&
                           newPos.z >= pathMinZ && newPos.z <= pathMaxZ);

            if (!inRespawnZone && !inForbidden && !inPath)
            {
                valid = true;
            }

        } while (!valid && attempts < 100);

        return newPos;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(forbiddenZoneCenter, respawnRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(forbiddenZoneCenter, forbiddenRadius);

        Gizmos.color = Color.yellow;
        Vector3 pathCenter = new Vector3(
            (pathMinX + pathMaxX) / 2f,
            0f,
            (pathMinZ + pathMaxZ) / 2f
        );
        Vector3 pathSize = new Vector3(
            pathMaxX - pathMinX,
            5f,
            pathMaxZ - pathMinZ
        );
        Gizmos.DrawWireCube(pathCenter, pathSize);
    }
}
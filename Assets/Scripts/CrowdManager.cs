using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject crowdMemberPrefab;
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 10;
    [SerializeField] private Vector2 spacing = new Vector2(1.5f, 1.2f);
    [SerializeField] private Vector2 randomOffset = new Vector2(0.3f, 0.1f);

    void Start()
    {
        SpawnCrowd();
    }

    void SpawnCrowd()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                // 1. Calculate the base position
                float posX = x * spacing.x;
                float posY = y * spacing.y;

                // 2. Add a little "human" randomness
                posX += Random.Range(-randomOffset.x, randomOffset.x);
                posY += Random.Range(-randomOffset.y, randomOffset.y);

                Vector3 spawnPos = new Vector3(posX, posY, 0);

                // 3. Instantiate and parent them to this object to keep the hierarchy clean
                GameObject fan = Instantiate(crowdMemberPrefab, transform.position + spawnPos, Quaternion.identity);
                fan.transform.parent = this.transform;

                // 4. Sort the layers so fans in front cover fans behind
                // Unity 2D uses 'Sorting Order'—higher numbers are in front
                if (fan.TryGetComponent<SpriteRenderer>(out var sr))
                {
                    sr.sortingOrder = -y; // Lower rows (higher Y) are further back
                }
            }
        }
    }
}
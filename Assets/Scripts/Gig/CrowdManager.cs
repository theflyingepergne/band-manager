using UnityEngine;
using UnityEngine.UIElements;

public class CrowdManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject crowdMember;
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

                // 2. Add randomness
                posX += Random.Range(-randomOffset.x, randomOffset.x);
                posY += Random.Range(-randomOffset.y, randomOffset.y);

                Vector3 spawnPos = new Vector3(posX, posY, 0);

                // 3. Instantiate and parent them to this object to keep the hierarchy clean
                GameObject crowdMemberClone = Instantiate(crowdMember, transform.position + spawnPos, Quaternion.identity);
                crowdMemberClone.transform.parent = this.transform;

                // y is the row index, so we can use it to determine which sprite to assign (0 = closest, 1 = middle, 2 = furthest)
                // if the number of rows is greater than 3, i want to remap the rows into three equal sections and assign sprites accordingly
                int spriteIndex = 0;
                if (rows > 3)
                {
                    int sectionSize = rows / 3;
                    if (y < sectionSize) spriteIndex = 0; // Closest
                    else if (y < 2 * sectionSize) spriteIndex = 1; // Middle
                    else spriteIndex = 2; // Furthest
                }
                else
                {
                    spriteIndex = y % 3;
                }

                CrowdMember cm = crowdMemberClone.GetComponent<CrowdMember>();
                cm.AssignSprite(spriteIndex);



                // 4. Sort the layers so crowdMemberClones in front cover crowdMemberClones behind
                // Unity 2D uses 'Sorting Order'—higher numbers are in front
                SpriteRenderer sr = crowdMemberClone.GetComponent<SpriteRenderer>();
                {
                    sr.sortingOrder = 10 + rows - y;
                    
                }
            }
        }
    }
}
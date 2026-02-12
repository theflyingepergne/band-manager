using UnityEngine;

public class CrowdMember : MonoBehaviour
{
    [SerializeField] private Sprite[] crowdSprites; // Array of different crowd member sprites
    // Array indices:
    // 0 = closest to camera
    // 1 = middle
    // 2 = furthest from camera

    public void AssignSprite(int index)
    {
        if (crowdSprites != null)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            // Assign sprite based on index passed from CrowdManager
            sr.sprite = crowdSprites[index];

        }
    }
}

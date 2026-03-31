using UnityEngine;

public class Ambulate : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector2 moveArea = new Vector2(9.5f, 5.1f);

    private Vector2 direction;
    private BoxCollider2D myCollider;

    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        Vector3 currentScale = transform.localScale;

        // "Is direction.x > 0? If YES, use Abs. If NO, use -Abs."
        currentScale.x = (direction.x > 0) ? Mathf.Abs(currentScale.x) : -Mathf.Abs(currentScale.x);

        transform.localScale = currentScale;

        // Get the "Half-Width" and "Half-Height" of the character's box
        float halfWidth = myCollider.bounds.extents.x;
        float halfHeight = myCollider.bounds.extents.y;

        // Bounce logic using the edges instead of the center
        if (transform.position.x + halfWidth > moveArea.x || transform.position.x - halfWidth < -moveArea.x)
        {
            direction.x *= -1;
            SnapBack(halfWidth, halfHeight);
        }

        if (transform.position.y + halfHeight > moveArea.y || transform.position.y - halfHeight < -moveArea.y)
        {
            direction.y *= -1;
            SnapBack(halfWidth, halfHeight);
        }
    }

    private void SnapBack(float hW, float hH)
    {
        Vector3 pos = transform.position;
        // Clamp taking the character's size into account
        pos.x = Mathf.Clamp(pos.x, -moveArea.x + hW, moveArea.x - hW);
        pos.y = Mathf.Clamp(pos.y, -moveArea.y + hH, moveArea.y - hH);
        transform.position = pos;
    }
}
using UnityEngine;

public class SetScreenPosition : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Distance in world units away from the right edge (positive = inside screen, negative = outside)")]
    [Range(-10f, 10f)]
    public float paddingFromEdge = 0.5f;
    [Range(0f, 1f)]
    public float screenPositionX = 1f;
    [Range(0f, 1f)]
    public float screenPositionY = 0.5f;

    //---Local References---//
    private Camera targetCamera;
    private int lastWidth;
    private int lastHeight;

    private void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        lastWidth = Screen.width;
        lastHeight = Screen.height;

        SetTransformAsScreenPosition();
    }

    public void SetTransformAsScreenPosition()
    {
        // Get the z-distance from the camera to this object so perspective doesn't warp position
        float distanceFromCamera = Mathf.Abs(transform.position.z - targetCamera.transform.position.z);

        // Get screen to world position
        Vector3 screenToWorldPosition = targetCamera.ViewportToWorldPoint(new Vector3(screenPositionX, screenPositionY, distanceFromCamera));

        // Add padding
        Vector3 newPosition = screenToWorldPosition - (new Vector3(1f, 1f, 0f) * paddingFromEdge);

        transform.position = newPosition;
    }

    private void Update()
    {
        if (Screen.width != lastWidth || Screen.height != lastHeight)
        {
            lastWidth = Screen.width;
            lastHeight = Screen.height;
        }

        SetTransformAsScreenPosition();
    }
}
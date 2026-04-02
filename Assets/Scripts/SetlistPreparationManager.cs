using UnityEngine;

public class SetlistPreparationManager : MonoBehaviour, IHoverable
{
    [SerializeField] private Vector3 startPosition = new Vector3(9.23f, -1.14f, 0f);
    [SerializeField] private Vector3 startRotation = new Vector3(0f, 0f, 5.41f);
    [SerializeField] private Vector3 hoverPosition = new Vector3(4.66f, -1.57f, 0f);
    [SerializeField] private Vector3 hoverRotation = new Vector3(0f, 0f, 12.295f);

    public void Start()
    {
        gameObject.transform.position = startPosition;
        gameObject.transform.rotation = Quaternion.Euler(startRotation);
    }
    public void OnIsHovering(bool isHovering)
    {
        gameObject.transform.position = isHovering ? hoverPosition : startPosition;
        gameObject.transform.localRotation = isHovering ? Quaternion.Euler(hoverRotation) : Quaternion.Euler(startRotation);  
    }
}

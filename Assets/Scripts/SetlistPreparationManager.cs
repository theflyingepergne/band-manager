using UnityEngine;
using DG.Tweening;

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
        Vector3 destPos = isHovering ? hoverPosition : startPosition;
        Vector3 destRot = isHovering ? hoverRotation : startRotation;

        // "Do Move" to the destination over 0.5 seconds with a "Bouncy" feel
        transform.DOMove(destPos, 0.5f).SetEase(Ease.OutBack);
        transform.DORotate(destRot, 0.5f).SetEase(Ease.OutBack);
    }
}

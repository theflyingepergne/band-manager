using UnityEngine;
using DG.Tweening;

public class CameraFade : MonoBehaviour
{
    public static CameraFade Instance { get; private set; }

    //---References---//
    [Header("UI References")]
    [SerializeField] private GameObject canvasBlack;

    [Header("Controls")]
    [SerializeField] private float fadeDuration = 0.2f;

    private void Awake()
    {
        Instance = this;
    }

    public void DoCameraFade(float alpha)
    {
        
        CanvasGroup cg = canvasBlack.GetComponent<CanvasGroup>();
        float startingAlpha = 1f - alpha;
        cg.alpha = startingAlpha;

        canvasBlack.SetActive(true);
        cg.DOFade(alpha, fadeDuration).OnComplete(FinishCameraFade);
    }
     private void FinishCameraFade()
    {
        canvasBlack.SetActive(false);
    }
}

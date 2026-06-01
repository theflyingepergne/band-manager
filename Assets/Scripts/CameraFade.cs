using UnityEngine;
using DG.Tweening;

public class CameraFade : MonoBehaviour
{
    public static CameraFade Instance { get; private set; }

    //---References---//
    [Header("UI References")]
    [SerializeField] private GameObject canvasBlack;

    [Header("Controls")]
    [SerializeField] private float fadeDuration = 0.5f;

    //---Methods---//
    private void Awake()
    {
        Instance = this;
    }

    public void DoCameraFade(int alpha)
    {
        // fade camera by tweening canvas group alpha
        CanvasGroup cg = canvasBlack.GetComponent<CanvasGroup>();
        float startingAlpha = 1f - alpha;
        cg.alpha = startingAlpha;

        // when the tween is complete, decide whether or not to "hold"
        canvasBlack.SetActive(true);
        cg.DOFade(alpha, fadeDuration).OnComplete(()=>FinishCameraFade(alpha));
    }
    
    private void FinishCameraFade(int hold)
    {
        // if fading to black (i.e alpha == 1), doHold == true
        bool doHold = System.Convert.ToBoolean(hold);
        canvasBlack.SetActive(doHold);
    }
}
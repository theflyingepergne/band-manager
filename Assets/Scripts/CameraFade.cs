using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CameraFade : MonoBehaviour
{
    public static CameraFade Instance;

    [Header("UI References")]
    [SerializeField] private CanvasGroup canvasGroupBlack;

    [Header("Controls")]
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private bool fadeInOnStart = true;

    private void Awake()
    {
        Instance = this;

        // Ensure it starts in the correct state
        if (fadeInOnStart)
        {
            canvasGroupBlack.gameObject.SetActive(true);
            canvasGroupBlack.alpha = 1f;
        }
        else
        {
            canvasGroupBlack.gameObject.SetActive(canvasGroupBlack.alpha > 0);
        }
    }

    private async void Start()
    {
        if (fadeInOnStart)
        {
            // 500ms delay starts the scene black
            await Task.Delay(200);
            
            // Fade from black
            await DoCameraFade(0);
        }
    }

    // As this is an async task, we must 'await' it when calling from other scripts
    public async Task DoCameraFade(float targetAlpha)
    {
        // Setup initial states
        canvasGroupBlack.gameObject.SetActive(true);

        // Run the tween and wait right here until it finishes
        await canvasGroupBlack.DOFade(targetAlpha, fadeDuration).AsyncWaitForCompletion();

        // Clean up if we faded out completely
        if (targetAlpha <= 0f)
        {
            canvasGroupBlack.gameObject.SetActive(false);
        }
    }
}
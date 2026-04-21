using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GigVibeBar : MonoBehaviour
{
    //---References---//
    [SerializeField] private Image fillImage;

    //---Local Refs---//
    private float targetFill;
    private float timeToFill;

    //---Methods---//
    public void SetupBar(float songScore, float songDuration)
    {
        targetFill = songScore / 100f;
        timeToFill = songDuration;

        FillBar();
    }

    public void FillBar()
    {
        fillImage.DOFillAmount(targetFill, timeToFill).SetEase(Ease.Linear);
    }
}
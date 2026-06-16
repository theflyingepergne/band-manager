using TMPro;
using UnityEngine;

public class CalendarIcon : MonoBehaviour, IClickable
{
    //---References---//
    [Header("Game Objects")]
    [SerializeField] private GameObject calendarCanvas;
    [SerializeField] private TextMeshPro numTextMesh;

    //---Local References---//
    DateManager dm;

    private void OnEnable() => DateManager.OnDateChanged += HandleDateChanged;
    private void OnDisable() => DateManager.OnDateChanged -= HandleDateChanged;

    private void Start()
    {
        dm = DateManager.Instance;
        HandleDateChanged(dm.date);
    }

    public void OnClicked()
    {
        calendarCanvas.SetActive(!calendarCanvas.activeSelf);
    }

    private void HandleDateChanged(GameDate date)
    {
        numTextMesh.text = date.day.ToString();
    }
}

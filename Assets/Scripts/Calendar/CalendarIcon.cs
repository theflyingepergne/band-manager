using TMPro;
using UnityEngine;

public class CalendarIcon : MonoBehaviour, IClickable
{
    //---References---//
    [Header("Game Objects")]
    [SerializeField] private Canvas calendarCanvas;
    [SerializeField] private TextMeshPro monthTextMesh;
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
        calendarCanvas.enabled = true;
    }

    private void HandleDateChanged(GameDate date)
    {
        // Set month text as abbreviate month
        monthTextMesh.text = date.GetAbbreviatedMonthAsString(date);
        numTextMesh.text = date.day.ToString();
    }
}

using UnityEngine;
using UnityEngine.UIElements;

public class PerformGigUIManager : Singleton<PerformGigUIManager>
{
    [SerializeField] private UIDocument performGigUI;

    private VisualElement root;
    private VisualElement setlistUI;

    public void Start()
    {
        root = performGigUI.rootVisualElement;
        setlistUI = root.Q<ScrollView>("Setlist");
        
        for (int i = 0; i < 5; i++)
        {
            Label songLabel = new Label();
            songLabel.text = (i + 1) + ". Song name " + (i + 1); // Eventually this will be actual songs written by 
            setlistUI.Add(songLabel);
        }
    }
}

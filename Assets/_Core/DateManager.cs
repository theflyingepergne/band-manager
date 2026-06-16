using UnityEngine;

public class DateManager
{
    public static DateManager Instance { get; private set; }

    //---Date---//
    public GameDate date = new( 30, 1, 1979 );

    //---Events---//
    public static System.Action<GameDate> OnDateChanged;

    //---Methods---//
    public static void Initialize()
    {
        Instance = new DateManager();
        Debug.Log("Initialized DateManager");
        // TODO: Load stats from SaveLoadSystem
        // Instance.LoadDate();
    }

    public void ChangeDate(int changeAmount)
    {
        date = date.AddDays(changeAmount);

        OnDateChanged?.Invoke(date);
    }
}
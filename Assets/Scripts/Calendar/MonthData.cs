public struct MonthData
{
    public string Name;
    public int Days;

    public MonthData(string name, int days)
    {
        Name = name;
        Days = days;
    }
}

public static class MonthList
{
    public static readonly MonthData[] Months =
    {
        new("Month = 0", 0),
        new("January", 31),
        new("February", 28),
        new("March", 31),
        new("April", 30),
        new("May", 31),
        new("June", 30),
        new("July", 31),
        new("August", 31),
        new("September", 30),
        new("October", 31),
        new("November", 30),
        new("December", 31)
    };
    // to use:
    // month = MonthList.Months[1]
    // monthName = month.Name; ("January")
    // monthDays = month.Days; (31)
}
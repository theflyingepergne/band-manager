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
        new MonthData("Month = 0", 0),
        new MonthData("January", 31),
        new MonthData("February", 28),
        new MonthData("March", 31),
        new MonthData("April", 30),
        new MonthData("May", 31),
        new MonthData("June", 30),
        new MonthData("July", 31),
        new MonthData("August", 31),
        new MonthData("September", 30),
        new MonthData("October", 31),
        new MonthData("November", 30),
        new MonthData("December", 31)
    };
    // to use:
    // month = MonthList.Months[1]
    // monthName = month.Name; ("January")
    // monthDays = month.Days; (31)
}
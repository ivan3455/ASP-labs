public class CurrentTimeService
{
    public string GetTimeOfDay()
    {
        DateTime current = DateTime.Now;
        int hour = current.Hour;

        if (hour >= 12 && hour < 18)
        {
            return "Зараз день";
        }
        else if (hour >= 18 && hour < 24)
        {
            return "Зараз вечір";
        }
        else if (hour >= 0 && hour < 6)
        {
            return "Зараз ніч";
        }
        else if (hour >= 6 && hour < 12)
        {
            return "Зараз ранок";
        }
        else {
            return "He вдалося визначити";
        }
    }
}
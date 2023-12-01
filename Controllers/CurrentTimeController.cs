public class CurrentTimeController
{
    private readonly CurrentTimeService currentTimeService;

    public CurrentTimeController(CurrentTimeService service)
    {
        currentTimeService = service;
    }

    public string GetCurrentTimeOfDay()
    {
        return currentTimeService.GetTimeOfDay();
    }
}

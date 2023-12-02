public class CalcController
{
    private CalcService calcService;

    // Приймає CalcService для обробки математичних операцій
    public CalcController(CalcService calcService)
    {
        this.calcService = calcService;
    }

    public double Add(double a, double b)
    {
        return calcService.Add(a, b);
    }

    public double Subtract(double a, double b)
    {
        return calcService.Subtract(a, b);
    }

    public double Divide(double a, double b)
    {
        return calcService.Divide(a, b);
    }

    public double Multiply(double a, double b)
    {
        return calcService.Multiply(a, b);
    }
}

public class CalcService
{
    public double Add(double a, double b)
    {
        return a + b;
    }

    public double Subtract(double a, double b)
    {
        return a - b;
    }

    // Викидає виключення, якщо друге число (b) дорівнює нулю
    public double Divide(double a, double b)
    {
        if (b == 0)
        {
            throw new ArgumentException("Ділення на нуль неможливе!");
        }

        return a / b;
    }

    public double Multiply(double a, double b)
    {
        return a * b;
    }
}

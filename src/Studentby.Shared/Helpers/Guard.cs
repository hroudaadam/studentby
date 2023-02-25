using System.Runtime.CompilerServices;

namespace Studentby.Shared.Helpers;

public static class Guard
{
    public static void NotNull(object argument, [CallerArgumentExpression("argument")] string argumentName = null)
    {
        if (argument == null)
            throw new ArgumentNullException(argumentName);
    }

    public static void NotNullOrEmpty<T>(IEnumerable<T> argument, [CallerArgumentExpression("argument")] string argumentName = null)
    {
        NotNull(argument, argumentName);
        NotEmpty(argument, argumentName);
    }

    private static void NotEmpty<T>(IEnumerable<T> argument, [CallerArgumentExpression("argument")] string argumentName = null)
    {
        if (!argument.Any())
            throw new ArgumentException("Collection is empty", argumentName);
    }

    public static void NotNullOrEmptyOrWhiteSpace(string argument, [CallerArgumentExpression("argument")] string argumentName = null)
    {
        NotNull(argument, argumentName);

        if (string.IsNullOrWhiteSpace(argument))
            throw new ArgumentException("String is empty or consists only of white-space characters");
    }

    public static void Max(int argument, int maxValue, [CallerArgumentExpression("argument")] string argumentName = null)
    {
        if (argument > maxValue)
            throw new ArgumentException($"Number is greater than {maxValue}", argumentName);
    }

    public static void Min(int argument, int minValue, [CallerArgumentExpression("argument")] string argumentName = null)
    {
        if (argument < minValue)
            throw new ArgumentException($"Number is smaller than {minValue}", argumentName);
    }

    public static void Between(int argument, int minValue, int maxValue, [CallerArgumentExpression("argument")] string argumentName = null)
    {
        Min(argument, minValue, argumentName);
        Max(argument, maxValue, argumentName);
    }
}
using Studentby.Shared.Helpers;
using System.Linq.Expressions;

namespace Studentby.Shared.Extensions;
public static class ExpressionExtensions
{
    public static string ToEvaluatedString(this Expression expression)
    {
        return ExpressionParser.ToString(expression);
    }
}

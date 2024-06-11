using System.Linq.Expressions;

namespace Shared.Util.Expression.Domain;

public class ExpressionConverter
{
    public static Dictionary<string, string> Convert<T>(Expression<Func<T, bool>> expression)
    {
        var result = new Dictionary<string, string>();

        var current = (BinaryExpression)expression.Body;

        while (current.NodeType != ExpressionType.Equal)
        {
            ParseEquals((BinaryExpression)current.Right);
            current = (BinaryExpression)current.Left;
        }

        ParseEquals(current);

        void ParseEquals(BinaryExpression e)
        {
            var key = (MemberExpression)e.Left;
            var value = (ConstantExpression)e.Right;
            result.Add(key.Member.Name, value.Value?.ToString() ?? "");
        }

        return result;
    }
}
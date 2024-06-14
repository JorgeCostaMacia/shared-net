using System.Linq.Expressions;

namespace Shared.Util.Expression.Domain;

public class ExpressionConverter
{
    public static Dictionary<string, string> Convert<T>(Expression<Func<T, bool>> expression)
    {
        Dictionary<string, string> Result = new Dictionary<string, string>();

        BinaryExpression Body = (BinaryExpression)expression.Body;

        while (Body.NodeType != ExpressionType.Equal)
        {
            ParseEquals((BinaryExpression)Body.Right);
            Body = (BinaryExpression)Body.Left;
        }

        ParseEquals(Body);

        void ParseEquals(BinaryExpression e)
        {
            MemberExpression Key = (MemberExpression)e.Left;
            ConstantExpression Value = (ConstantExpression)e.Right;

            Result.Add(Key.Member.Name, Value.Value?.ToString() ?? "");
        }

        return Result;
    }
}
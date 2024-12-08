using System.Linq.Expressions;

namespace Shared.Util.Expression.Domain
{
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

        public static Expression<Func<T, bool>> ConvertBack<T>(Dictionary<string, string> dictionary)
        {
            ParameterExpression parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), "x");

            List<System.Linq.Expressions.Expression> expressions = new List<System.Linq.Expressions.Expression>();

            foreach (KeyValuePair<string, string> d in dictionary)
            {
                MemberExpression property = System.Linq.Expressions.Expression.Property(parameter, d.Key);

                object? convertedValue = System.Convert.ChangeType(d.Value, property.Type);
                ConstantExpression constant = System.Linq.Expressions.Expression.Constant(convertedValue, property.Type);

                BinaryExpression equality = System.Linq.Expressions.Expression.Equal(property, constant);
                expressions.Add(equality);
            }

            System.Linq.Expressions.Expression? body = expressions.Count > 1 ? expressions.Aggregate(System.Linq.Expressions.Expression.AndAlso) : expressions.First();

            return System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
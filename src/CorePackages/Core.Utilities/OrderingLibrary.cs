using System.Linq.Expressions;
using System.Reflection;

namespace Core.Utilities;
public static class OrderingLibrary
{
    public static Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderBy<T>(string orderColumn, string orderType)
    {
        Type typeQueryable = typeof(IQueryable<T>);
        ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");
        var outerExpression = Expression.Lambda(argQueryable, argQueryable);
        string[] props = orderColumn.Split('.');
        IQueryable<T> query = new List<T>().AsQueryable<T>();
        Type type = typeof(T);
        ParameterExpression arg = Expression.Parameter(type, "x");

        Expression expr = arg;
        foreach (string prop in props)
        {
            PropertyInfo pi = type?.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (pi != null)
            {
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
        }
        LambdaExpression lambda = Expression.Lambda(expr, arg);
        string methodName = orderType == "asc" ? "OrderBy" : "OrderByDescending";

        MethodCallExpression resultExp =
            Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(T), type }, outerExpression.Body, Expression.Quote(lambda));
        var finalLambda = Expression.Lambda(resultExp, argQueryable);
        return (Func<IQueryable<T>, IOrderedQueryable<T>>)finalLambda.Compile();
    }

    private static IOrderedQueryable<T> OrderingHelper<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
    {
        ParameterExpression param = Expression.Parameter(typeof(T), string.Empty); // I don't care about some naming
        MemberExpression property = Expression.PropertyOrField(param, propertyName);
        LambdaExpression sort = Expression.Lambda(property, param);
        MethodCallExpression call = Expression.Call(
            typeof(Queryable),
            (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
            new[] { typeof(T), property.Type },
            source.Expression,
            Expression.Quote(sort));
        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
    }

    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
    {
        return OrderingHelper(source, propertyName, false, false);
    }
    public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
    {
        return OrderingHelper(source, propertyName, true, false);
    }
    public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
    {
        return OrderingHelper(source, propertyName, false, true);
    }
    public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName)
    {
        return OrderingHelper(source, propertyName, true, true);
    }


}
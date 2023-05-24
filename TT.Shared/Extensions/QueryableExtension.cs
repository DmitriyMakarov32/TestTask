using System.Linq.Expressions;

namespace TT.Shared.Extensions;

public static class QueryableExtension
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> query,
        bool condition,
        Expression<Func<T, bool>> predicat)
    {
        if (query == null)
            throw new ArgumentNullException(nameof (query));
        if (predicat == null)
            throw new ArgumentNullException(nameof (predicat));
        return !condition ? query : query.Where<T>(predicat);
    }
}
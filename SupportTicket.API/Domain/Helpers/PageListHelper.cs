using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SupportTicket.API.Domain.Helpers.Models;

public enum OrderByDirection
{
    None,
    Ascending,
    Descending
}

public class PageInfo
{
    public PageInfo()
    {
    }

    public PageInfo(int pageNumber, int pageSize, OrderByDirection orderBy, string sortByProperty)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        OrderBy = orderBy;
        SortByProperty = sortByProperty;
    }

    public int PageSize { get; set; } = 50;

    public int PageNumber { get; set; } = 0;

    public string SortByProperty { get; set; } = null;

    public OrderByDirection OrderBy { get; set; } = OrderByDirection.None;
}

public class PageList<T>
{
    public PageInfo PageInfo { get; set; }

    public int TotalPages { get; set; }

    public int TotalItems { get; set; }

    public List<T> Items { get; set; }

    public static async Task<PageList<T>> Create(PageInfo? pageInfo, IQueryable<T> query = null)
    {
        pageInfo = pageInfo ?? new PageInfo();

        if (query == null)
        {
            return new PageList<T>()
            {
                PageInfo = pageInfo,
                TotalItems = 0,
                TotalPages = 0,
                Items = new List<T>()
            };
        }

        var totalCount = await query.CountAsync();

        query = ApplyOrder(query, pageInfo.SortByProperty, pageInfo.OrderBy);
        query = query.Skip(pageInfo.PageNumber * pageInfo.PageSize).Take(pageInfo.PageSize);

        var items = await query.ToListAsync();

        return new PageList<T>()
        {
            PageInfo = pageInfo,
            TotalItems = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageInfo.PageSize),
            Items = items
        };
    }

    private static IQueryable<T> ApplyOrder(
        IQueryable<T> query,
        string propertyName,
        OrderByDirection direction)
    {
        if (string.IsNullOrWhiteSpace(propertyName) || direction == OrderByDirection.None)
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var lambda = Expression.Lambda(property, parameter);

        string methodName = direction == OrderByDirection.Ascending ? "OrderBy" : "OrderByDescending";

        var orderByExpression = Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { typeof(T), property.Type },
            query.Expression,
            Expression.Quote(lambda));

        return query.Provider.CreateQuery<T>(orderByExpression);
    }
}

public abstract class PageListTypeBase<T, TGraphType> : ObjectGraphType<PageList<T>>
    where TGraphType : IGraphType
{
    public PageListTypeBase()
    {
        Description = "The paged data to return.";

        Field<PageInfoType>("pageInfo",
            resolve: context => context.Source.PageInfo,
            description: "The page info that was used.");
        Field(x => x.TotalItems).Description("The number all items in the data set.");
        Field(x => x.TotalPages).Description("The number of total pages.");
        Field<ListGraphType<TGraphType>>("items",
            resolve: context => context.Source.Items,
            description: "The paged data items from the set.");
    }
}

public class PageInfoInputType : InputObjectGraphType<PageInfo>
{
    public PageInfoInputType()
    {
        Description = "The page information to return.";

        Field(x => x.PageSize).Description("The size of a single page.").DefaultValue(50);
        Field(x => x.PageNumber).Description("The number of the current page.").DefaultValue(0);
        Field(x => x.OrderBy).Description("The sort order direction.").DefaultValue(OrderByDirection.None);
        Field(x => x.SortByProperty).Description("The property name to sort by.").DefaultValue(null);
    }
}

public class PageInfoType : ObjectGraphType<PageInfo>
{
    public PageInfoType()
    {
        Description = "The page information to return.";

        Field(x => x.PageSize).Description("The size of a single page.").DefaultValue(50);
        Field(x => x.PageNumber).Description("The number of the current page.").DefaultValue(0);
        Field(x => x.OrderBy).Description("The sort order direction.").DefaultValue(OrderByDirection.None);
        Field(x => x.SortByProperty).Description("The property name to sort by.").DefaultValue(null);
    }
}
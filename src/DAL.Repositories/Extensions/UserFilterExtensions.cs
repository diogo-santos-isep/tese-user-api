namespace DAL.Repositories.Extensions
{
    using Models.Domain.Models;
    using Models.Filters;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class UserFilterExtensions
    {
        public static FilterDefinition<User> BuildFilters(this UserFilter filter)
        {
            var arr = new List<FilterDefinition<User>>();
            if (!String.IsNullOrEmpty(filter.Department_Id))
                arr.Add(Builders<User>.Filter.Eq(x => x.Department_Id, filter.Department_Id));
            if (arr.Count == 0)
                arr.Add(Builders<User>.Filter.Empty);
            return Builders<User>.Filter.And(arr);
        }
        public static SortDefinition<T> BuildSort<T>(this Filter filter)
        {
            return filter.SortAscending ? Builders<T>.Sort.Ascending(filter.SortBy) : Builders<T>.Sort.Descending(filter.SortBy);
        }
    }
}
